using ApiBase.Application.ApplicationGuid;
using Domain;

namespace Application.RolePermissions
{
    public interface IAplicRolePermission : IApplicationGuid<RolePermissionView>
    {
        void BindRolePermission(Guid roleId, Guid permissionId);
        void RemoveBindRolePermission(Guid id);
    }
}
