using ApiBase.Application.ApplicationGuid;
using Domain;

namespace Application.Permissions
{
    public interface IAplicPermission : IApplicationGuid<PermissionView>
    {
        PermissionView NewPermission(NewPermissionDTO dto);
        PermissionView UpdatePermission(Guid id, UpdatePermissionDTO dto);
        void RemovePermission(Guid id);
    }
}
