using ApiBase.Application.ApplicationGuid;
using Domain;

namespace Application.UserRoles
{
    public interface IAplicUserRole : IApplicationGuid<UserRoleView>
    {
        void BindUserRole(Guid userId, Guid roleId);
        void RemoveBindUserRole(Guid id);
    }
}
