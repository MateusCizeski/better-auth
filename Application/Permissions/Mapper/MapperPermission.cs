using Domain;

namespace Application.Permissions
{
    public class MapperPermission : IMapperPermission
    {
        public Permission NewPermission(NewPermissionDTO dto)
        {
            return new Permission
            {
                Key = dto.Key,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public PermissionView ToView(Permission permission)
        {
            return new PermissionView
            {
                Id = permission.Id,
                Key = permission.Key,
                Description = permission.Description,
                CreatedAt = permission.CreatedAt,
                UpdatedAt = permission.UpdatedAt,
            };
        }

        public void UpdatePermission(Permission permission, UpdatePermissionDTO dto)
        {
            permission.Key = dto.Key;
            permission.Description = dto.Description;
        }
    }
}
