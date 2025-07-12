using Domain.User;
using Domain.User.DTOs;

namespace Application.Users
{
    public interface IMapperUser
    {
        User NewUser(CreateUserDTO dto, string passwordHash);
        void UpdateUser(User user, UpdateUserDTO dto);
    }
}
