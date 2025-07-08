using Application.User.DTOs;
using Domain.User;

namespace Infrastructure.Repositories
{
    public interface IRepUser
    {
        UserDetailDTO DetailUser(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task AuthUser(AuthUserDTO dto);
    }
}
