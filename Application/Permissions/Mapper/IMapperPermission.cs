using Domain;

namespace Application.Permissions
{
    public interface IMapperPermission
    {
        Permission NewPermission(NewPermissionDTO dto);
        void UpdatePermission(Permission permission, UpdatePermissionDTO dto);
        PermissionView ToView(Permission permission);
    }
}
