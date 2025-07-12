using Domain.User.DTOs;
using Domain.User;

namespace Infrastructure.Repositories
{
    public interface IRepUser
    {
        UserDetailDTO DetailUser(Guid id);
        UserDetailDTO CreateUser(User user);
        UserDetailDTO UpdateUser(User user);
        Task AuthUser(AuthUserDTO dto);
        User SearchUserById(Guid id);
    }
}
