using Domain.User.DTOs;
using Domain.Users;
using Infra.Helper;

namespace Application.Users
{
    public class MapperUser : IMapperUser
    {
        public User NewUser(NewUserDTO dto)
        {
            var (hash, salt) = PasswordHelper.HashPassword(dto.Password);

            return new User
            {
                Name = dto.Name,    
                Email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };
        }

        public UserView ToView(User user)
        {
            return new UserView
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                IsActive = user.IsActive
            };
        }

        public void UpdateUser(User user, UserUpdateSelfDto dto)
        {
            user.Name = dto.Name;
            user.Email = dto.Email;
        }
    }
}
