using Domain;

namespace Application.RolePermissions
{
    public class MapperRolePermission : IMapperRolePermission
    {
        public RolePermission NewRolePermission(Guid roleId, Guid permissionId)
        {
            return new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}
