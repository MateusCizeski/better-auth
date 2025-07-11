using Domain.User;

namespace Application.Users
{
    public class MapperUser : IMapperUser
    {
        public User NewUser(CreateUserDTO dto)
        {
            return new User()
            {
                PasswordHash = dto.Password,
                IsActive = true,
                Name = dto.Name,
                Email = dto.Email,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            };
        }
    }
}
