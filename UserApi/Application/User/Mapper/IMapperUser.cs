using Application.User.DTOs;
using Domain.User;

namespace Application.Users
{
    public interface IMapperUser
    {
        User NewUser(CreateUserDTO dto);
    }
}
