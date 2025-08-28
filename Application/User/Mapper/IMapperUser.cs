using Domain.RefreshTokens;
using Domain.User.DTOs;
using Domain.Users;

namespace Application.Users
{
    public interface IMapperUser
    {
        User NewUser(NewUserDTO dto);
        void UpdateUser(User user, UserUpdateSelfDto dto);
        UserView ToView(User user);
        RefreshToken NewRefreshToken(User user, string ipAddress);
    }
}
