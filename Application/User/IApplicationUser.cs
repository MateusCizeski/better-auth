using ApiBase.Application.ApplicationGuid;
using Domain;

namespace Application.Users
{
    public interface IApplicationUser : IApplicationGuid<UserView>
    {
        UserView NewUser(NewUserDTO dto);
        UserView UpdateUser(Guid id, UserUpdateSelfDto dto);
        LoginResultDTO Login(LoginDTO dto, string ipAddress, string deviceId, string userAgent);
        LoginResultDTO Refresh(string token, string ipAddress, string deviceId, string userAgent);
        void Logout(string refreshToken, string ipAddress, string accessToken);
        IEnumerable<SessionViewDTO> GetSessions(Guid userId);
        void RevokeSession(Guid userId, string deviceId);
    }
}
