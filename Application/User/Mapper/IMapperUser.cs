using Domain.User.DTOs;
using Domain.Users;

namespace Application.Users
{
    public interface IMapperUser
    {
        User NewUser(NewUserDTO dto);
        UserView ToView(User user);
    }
}
