using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using ApiBase.Infra.Extensions;
using Domain;
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
        public ApplicationUser(IUnitOfWork unitOfWork,
                               IRepositoryUser repository,
                               IMapperUser mapperUser,
                               IRepRefreshToken repRefreshToken,
                               IOptions<JwtSettings> jwtOptions) : base(unitOfWork, repository)
        {
            _repositoryUser = repository;
            _mapperUser = mapperUser;
            _jwtSettings = jwtOptions.Value;
            _repRefreshToken = repRefreshToken;
        }

        public LoginResultDTO Login(LoginDTO dto, string ipAddress, string deviceId, string userAgent)
        {
            var user = _repositoryUser.Get().Where(p => p.Email == dto.Email).FirstOrDefault();

            if (user == null || user.IsDeleted)
            {
                throw new UnauthorizedAccessException("Invalid or inactive user.");
            }

            if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Account temporarily locked due to multiple failed attempts.");
            }

            if (!PasswordHelper.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                user.FailedLoginAttempts++;

                if (user.FailedLoginAttempts >= 5)
                {
                    user.LockoutEnd = DateTime.UtcNow.AddMinutes(10);
                    Commit();
                    throw new UnauthorizedAccessException("Too many failed attempts. Account locked for 10 minutes.");
                }

                Commit();
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", user.Name),
                new Claim("id", user.Id.ToString())
            };

            var token = JwtTokenHelper.GenerateToken(claims, _jwtSettings.SecretKey, _jwtSettings.Issuer, _jwtSettings.Audience, 24);
            var refreshToken = _mapperUser.NewRefreshToken(user, ipAddress, deviceId, userAgent);
            user.FailedLoginAttempts = 0;
            user.LockoutEnd = null;
            user.LastLoginAt = DateTime.UtcNow;

            _repRefreshToken.Insert(refreshToken);
            Commit();

            return new LoginResultDTO
            {
                Id = user.Id,
                Token = token,
                Email = user.Email,
                Name = user.Name,
                UserName = user.UserName,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }

        public UserView NewUser(NewUserDTO dto)
        {
            if (_repositoryUser.Get().Any(u => u.Email == dto.Email))
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            var userName = UsernameGenerator.Generate(
                dto.Name,
                username => _repositoryUser.Get().Any(u => u.UserName == username)
            );

            var user = _mapperUser.NewUser(dto, userName);

            _repositoryUser.Insert(user);
            Commit();

            return _mapperUser.ToView(user);
        }

        public LoginResultDTO Refresh(string token, string ipAddress, string deviceId, string userAgent)
        {
            var refresh = _repRefreshToken.Get().FirstOrDefault(r => r.Token == token);

            if (refresh.CreatedByIp != ipAddress || refresh.DeviceId != deviceId || refresh.UserAgent != userAgent)
            {
                throw new UnauthorizedAccessException("Refresh token context does not match this device.");
            }

            if (refresh == null || refresh.Revoked != null || refresh.Expires <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            var user = _repositoryUser.GetById(refresh.UserId).uExceptionSeNull("User not found.");
            var accessToken = JwtTokenHelper.GenerateToken(user.Id, user.Email, user.Name, _jwtSettings.SecretKey, _jwtSettings.Issuer, _jwtSettings.Audience, 15);

            var newRefresh = _mapperUser.NewRefreshToken(user, ipAddress, deviceId, userAgent);

            refresh.Revoked = DateTime.UtcNow;
            refresh.RevokedByIp = ipAddress;
            refresh.ReplacedByToken = newRefresh.Token;

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

            _mapperUser.UpdateUser(user, dto);
            Commit();

            return _mapperUser.ToView(user);
        }
    }
}
