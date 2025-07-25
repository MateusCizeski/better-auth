using Domain.User.DTOs;
using Domain.User;

namespace Infrastructure.Repositories
{
    public interface IRepUser
    {
        string AuthUser(AuthUserDTO dto);
        User SearchUserById(Guid id);
    }
}
