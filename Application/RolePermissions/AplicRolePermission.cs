using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using Domain;

namespace Application.RolePermissions
{
    public class AplicRolePermission : ApplicationGuid<RolePermission, IRepRolePermission, RolePermissionView>
    {
        private readonly IRepRolePermission _repRolePermission;
        public AplicRolePermission(IUnitOfWork unitOfWork, IRepRolePermission repository) : base(unitOfWork, repository)
        {
            _repRolePermission = repository;
        }
    }
}
