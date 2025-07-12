using Domain.User.DTOs;
using Domain.User;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class RepUser : IRepUser
    {
        private readonly ApplicationDbContext _context;
        public RepUser(ApplicationDbContext context)
        {
            _context = context;
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

        public Task AuthUser(AuthUserDTO dto)
        {
            throw new NotImplementedException();
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
