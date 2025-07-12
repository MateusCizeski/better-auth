using Domain.User;
using Domain.User.DTOs;

namespace Application.Users
{
    public class MapperUser : IMapperUser
    {
        public User NewUser(CreateUserDTO dto, string passwordHash)
        {
            return new User()
            {
                PasswordHash = passwordHash,
                IsActive = true,
                Name = dto.Name,
                Email = dto.Email,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public void UpdateUser(User user, UpdateUserDTO dto)
        {
            user.Name = dto.Name;
            user.IsActive = dto.IsActive;
            user.UpdatedAt = DateTime.UtcNow;
        }
    }
}
