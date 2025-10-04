using Domain;

namespace Application.Roles
{
    public interface IMapperRole
    {
        Role NewRole(NewRoleDTO dto);
        void UpdateUser(Role role, UpdateRoleDTO dto);
        RoleView ToView(Role role);
    }
}
