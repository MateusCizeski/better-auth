using Domain.User.DTOs;
using Domain.Users;

namespace Application.Users
{
    public class MapperUser : IMapperUser
    {
        public User NewUser(NewUserDTO dto)
        {
            return new User
            {
                Name = dto.Name,    
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
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
    }
}
