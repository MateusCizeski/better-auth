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

        public User NewUser(NewUserDTO dto, string username)
        {
            var (hash, salt) = PasswordHelper.HashPassword(dto.Password);

            return new User
            {
                Name = dto.Name,
                UserName = username,
                Email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                EmailConfirmed = false
            };
        }

        public UserView ToView(User user)
        {
            return new UserView
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                IsDeleted = user.IsDeleted
            };
        }

        public void UpdateUser(User user, UserUpdateSelfDto dto)
        {
            user.Name = dto.Name;
        }
    }
}
