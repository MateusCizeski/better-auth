using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using ApiBase.Infra.Extensions;
using Domain;
using Domain.BlacklistedTokens;
using Infra.Helper;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Users
{
    public class ApplicationUser : ApplicationGuid<User, IRepositoryUser, UserView>, IApplicationUser
    {
        private readonly IMapperUser _mapperUser;
        private readonly JwtSettings _jwtSettings;
        private readonly IRepositoryUser _repositoryUser;
        private readonly IRepRefreshToken _repRefreshToken;
        private readonly IRepBlacklistedToken _repBlacklistedToken;
        private readonly IRepRolePermission _repRolePermission;
        private readonly IRepPermission _repPermission;
        private readonly IRepUserRole _repUserRole;
        public ApplicationUser(IUnitOfWork unitOfWork, 
                               IRepositoryUser repository, 
                               IMapperUser mapperUser,
                               IRepRefreshToken repRefreshToken,
                               IOptions<JwtSettings> jwtOptions,
                               IRepBlacklistedToken repBlacklistedToken,
                               IRepRolePermission repRolePermission,
                               IRepPermission repPermission,
                               IRepUserRole repUserRole) : base(unitOfWork, repository)
        {
            _repositoryUser = repository;
            _mapperUser = mapperUser;
            _jwtSettings = jwtOptions.Value;
            _repRefreshToken = repRefreshToken;
            _repBlacklistedToken = repBlacklistedToken;
            _repRolePermission = repRolePermission;
            _repPermission= repPermission;
            _repUserRole = repUserRole;
        }

        public LoginResultDTO Login(LoginDTO dto, string ipAddress, string deviceId, string userAgent)
        {
            var user = _repositoryUser.Get().Where(p => p.Email == dto.Email).FirstOrDefault();

            if(user == null || !user.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid or inactive user.");
            }

            if(!PasswordHelper.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var userRoles = _repUserRole.Get()
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.Role.Name)
                .ToList();

            var userPermissions = (
                from ur in _repUserRole.Get()
                join rp in _repRolePermission.Get() on ur.RoleId equals rp.RoleId
                join p in _repPermission.Get() on rp.PermissionId equals p.Id
                where ur.UserId == user.Id
                select p.Key).Distinct().ToList();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", user.Name),
                new Claim("id", user.Id.ToString())
            };

            claims.AddRange(userRoles.Select(r => new Claim("role", r)));
            claims.AddRange(userPermissions.Select(p => new Claim("permission", p)));

            var token = JwtTokenHelper.GenerateToken(user.Id, user.Email, user.Name, _jwtSettings.SecretKey, _jwtSettings.Issuer, _jwtSettings.Audience, 24);
            var refreshToken = _mapperUser.NewRefreshToken(user, ipAddress, deviceId, userAgent);

            _repRefreshToken.Insert(refreshToken);
            Commit();

            return new LoginResultDTO
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }

        public void Logout(string refreshToken, string ipAddress, string accessToken)
        {
            var token = _repRefreshToken.Get().FirstOrDefault(p => p.Token == refreshToken);

            if (token == null) return;

            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;

            var blacklisted = new BlacklistedToken
            {
                Token = accessToken,
                UserId = token.UserId,
                RevokedAt = DateTime.UtcNow
            };

            _repBlacklistedToken.Insert(blacklisted);

            Commit();
        }

        public UserView NewUser(NewUserDTO dto)
        {
            if(_repositoryUser.Get().Any(u => u.Email == dto.Email))
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            if (_repositoryUser.Get().Any(u => u.UserName == dto.UserName))
            {
                throw new InvalidOperationException("Username is already taken.");
            }

            var user = _mapperUser.NewUser(dto);

            _repositoryUser.Insert(user);
            Commit();

            return _mapperUser.ToView(user);
        }

        public LoginResultDTO Refresh(string token, string ipAddress, string deviceId, string userAgent)
        {
            var refresh = _repRefreshToken.Get().FirstOrDefault(r => r.Token == token);

            if (refresh == null || refresh.Revoked != null || refresh.Expires <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            var user = _repositoryUser.GetById(refresh.UserId).uExceptionSeNull("User not found.");
            var accessToken = JwtTokenHelper.GenerateToken(user.Id, user.Email, user.Name, _jwtSettings.SecretKey, _jwtSettings.Issuer, _jwtSettings.Audience, 24);

            refresh.Revoked = DateTime.UtcNow;
            refresh.RevokedByIp = ipAddress;
            refresh.ReplacedByToken = token;

           var newRefresh = _mapperUser.NewRefreshToken(user, ipAddress, userAgent, deviceId);

            _repRefreshToken.Insert(newRefresh);
            Commit();

            return new LoginResultDTO
            {
                Token = accessToken,
                RefreshToken = newRefresh.Token,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }

        public UserView UpdateUser(Guid id, UserUpdateSelfDto dto)
        {
            var user = _repositoryUser.GetById(id).uExceptionSeNull("User not found.");

            if (!string.Equals(user.UserName, dto.Username, StringComparison.OrdinalIgnoreCase))
            {
                bool exists = _repositoryUser.Get().Any(u => u.UserName == dto.Username && u.Id != id);

                if (exists) throw new InvalidOperationException("Username is already taken.");
            }

            _mapperUser.UpdateUser(user, dto);
            Commit();

            return _mapperUser.ToView(user);
        }

        public IEnumerable<SessionViewDTO> GetSessions(Guid userId)
        {
            return _repRefreshToken.Get()
                .Where(p => p.UserId == userId)
                .Select(p => new SessionViewDTO
                {
                    DeviceId = p.DeviceId,
                    UserAgent = p.UserAgent,
                    Created = p.Created,
                    Revoked = p.Revoked,
                    Expires = p.Expires,
                    IpAddress = p.CreatedByIp
                }).ToList();
        }

        public void RevokeSession(Guid userId, string deviceId)
        {
            var session = _repRefreshToken.Get().FirstOrDefault(p => p.UserId == userId && 
                                                                     p.DeviceId == deviceId);

            if (session == null) throw new InvalidOperationException("Session not found.");

            session.Revoked = DateTime.UtcNow;

            Commit();
        }

        public IEnumerable<string> GetUserPermissions(Guid userId)
        {
            var permissions = (from ur in _repUserRole.Get()
                               join rp in _repRolePermission.Get() on ur.RoleId equals rp.RoleId
                               join p in _repPermission.Get() on rp.PermissionId equals p.Id
                               where ur.UserId == userId
                               select p.Key).Distinct().ToList();

            return permissions;
        }
    }
}
