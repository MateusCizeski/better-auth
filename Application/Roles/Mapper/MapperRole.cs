using Domain;

namespace Application.Roles
{
    public class MapperRole : IMapperRole
    {
        public Role NewRole(NewRoleDTO dto)
        {
            return new Role
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public RoleView ToView(Role role)
        {
            return new RoleView
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt,
            };
        }
    }
}
