using Domain;

namespace Application.Roles
{
    public interface IMapperRole
    {
        Role NewRole(NewRoleDTO dto);
        RoleView ToView(Role role);
    }
}
