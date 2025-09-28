using Domain;

namespace Application.Users
{
    public interface IMapperUser
    {
        User NewUser(NewUserDTO dto);
        void UpdateUser(User user, UserUpdateSelfDto dto);
        UserView ToView(User user);
        RefreshToken NewRefreshToken(User user, string ipAddress, string deviceId, string userAgent);
    }
}
