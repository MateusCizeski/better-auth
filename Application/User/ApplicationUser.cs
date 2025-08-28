using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using ApiBase.Infra.Extensions;
using Domain.Jwt;
using Domain.RefreshTokens;
using Domain.User.DTOs;
using Domain.Users;
using Infra.Helper;
using Microsoft.Extensions.Options;

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

        public LoginResultDTO Login(LoginDTO dto, string ipAddress)
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

            var token = JwtTokenHelper.GenerateToken(user.Id, user.Email, user.Name, _jwtSettings.SecretKey, _jwtSettings.Issuer, _jwtSettings.Audience, 24);
            var refreshToken = _mapperUser.NewRefreshToken(user, ipAddress);

            _repRefreshToken.Insert(refreshToken);
            Commit();

            return new LoginResultDTO
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }

        public void Logout(string refreshToken, string ipAddress)
        {
            throw new NotImplementedException();
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

        public LoginResultDTO Refresh(string token, string ipAddress)
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

           var newRefresh = _mapperUser.NewRefreshToken(user, ipAddress);

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
    }
}
