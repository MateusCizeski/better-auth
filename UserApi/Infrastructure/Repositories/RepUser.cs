using Application.User.DTOs;
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

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public Task AuthUser(AuthUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
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

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
