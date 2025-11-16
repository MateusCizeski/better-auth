using Domain;
using Infra.Helper;

namespace Application.Users
{
    public class MapperUser : IMapperUser
    {
        public RefreshToken NewRefreshToken(User user, string ipAddress, string deviceId, string userAgent)
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString("N"),
                UserId = user.Id,
                Expires = DateTime.UtcNow.AddDays(31),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

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
                IsActive = true,
                EmailConfirmed = false
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
        }
    }
}
