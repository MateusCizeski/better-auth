using Domain;

namespace Application.RolePermissions
{
    public interface IMapperRolePermission
    {
        RolePermission NewRolePermission(Guid roleId, Guid permissionId);
    }
}
