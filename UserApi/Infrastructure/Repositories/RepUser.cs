using Domain.User.DTOs;
using Domain.User;
using Infrastructure.Data;
using CrossCutting.JWT;

namespace Infrastructure.Repositories
{
    public class RepUser : IRepUser
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        public RepUser(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public UserDetailDTO CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return new UserDetailDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                IsActive = user.IsActive
            };
        }

        public string AuthUser(AuthUserDTO dto)
        {
            var user = _context.Users.Where(u => u.Email == dto.Email).FirstOrDefault();

            if(user == null || BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new Exception("Email/Password incorrect.");
            }

            var token = _tokenService.GenerateToken(user);

            return token;
        }

        public UserDetailDTO DetailUser(Guid id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user == null) throw new Exception("User not exists.");

            return new UserDetailDTO
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                IsActive = user.IsActive,
                Name = user.Name,
                UpdatedAt = user.UpdatedAt
            };
        }

        public UserDetailDTO UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChangesAsync();

            return new UserDetailDTO
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                IsActive = user.IsActive,
                Name = user.Name,
                UpdatedAt = user.UpdatedAt
            };
        }

        public User SearchUserById(Guid id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user == null) throw new Exception("User not exists.");

            return user;
        }
    }
}
