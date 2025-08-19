using ApiBase.Application.ApplicationGuid;
using Domain.User.DTOs;
using Domain.Users;

namespace Application.Users
{
    public interface IApplicationUser : IApplicationGuid<UserView>
    {
        UserView NewUser(NewUserDTO dto);
    }
}
