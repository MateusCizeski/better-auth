using Domain;

namespace Application.UserRoles
{
    public interface IMapperUserRole
    {
        UserRole NewUserRole(Guid userId, Guid roleId);
    }
}
