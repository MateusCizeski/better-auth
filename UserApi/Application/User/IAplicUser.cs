using Domain.User.DTOs;

namespace Application.Users
{
    public interface IAplicUser
    {
        UserDetailDTO CreateUser(CreateUserDTO dto);
        UserDetailDTO DetailUser(Guid id);
        UserDetailDTO UpdateUser(Guid id, UpdateUserDTO dto);
        string AuthUser(AuthUserDTO dto);
    }
}
