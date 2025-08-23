using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using Domain.Jwt;
using Domain.User.DTOs;
using Domain.Users;
using Infra.Helper;
using Microsoft.Extensions.Options;

namespace Application.Users
{
    public class ApplicationUser : ApplicationGuid<User, IRepositoryUser, UserView>, IApplicationUser
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly IMapperUser _mapperUser;
        private readonly JwtSettings _jwtSettings;
        public ApplicationUser(IUnitOfWork unitOfWork, 
                               IRepositoryUser repository, 
                               IMapperUser mapperUser,
                               IOptions<JwtSettings> jwtOptions) : base(unitOfWork, repository)
        {
            _repositoryUser = repository;
            _mapperUser = mapperUser;
            _jwtSettings = jwtOptions.Value;
        }

        public LoginResultDTO Login(LoginDTO dto)
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

            return new LoginResultDTO
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }

        public UserView NewUser(NewUserDTO dto)
        {
            var user = _mapperUser.NewUser(dto);

            _repositoryUser.Insert(user);
            Commit();

            return _mapperUser.ToView(user);
        }

        public UserView UpdateUser(Guid id, UserUpdateSelfDto dto)
        {
            var user = _repositoryUser.GetById(id);

            _mapperUser.UpdateUser(user, dto);
            Commit();

            return _mapperUser.ToView(user);
        }
    }
}
