using ApiBase.Application.ApplicationGuid;
using Domain.User.DTOs;
using Domain.Users;

namespace Application.Users
{
    public interface IApplicationUser : IApplicationGuid<UserView>
    {
        UserView NewUser(NewUserDTO dto);
        UserView UpdateUser(Guid id, UserUpdateSelfDto dto);
        LoginResultDTO Login(LoginDTO dto, string ipAddress);
        LoginResultDTO Refresh(string token, string ipAddress);
        void Logout(string refreshToken, string ipAddress);
    }
}
