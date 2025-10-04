using ApiBase.Application.ApplicationGuid;
using Domain;

namespace Application.Roles
{
    public interface IAplicRole : IApplicationGuid<RoleView>
    {
        RoleView NewRole(NewRoleDTO dto);
        RoleView UpdateRole(Guid id, UpdateRoleDTO dto);
        void RemoveRole(Guid id);
    }
}
