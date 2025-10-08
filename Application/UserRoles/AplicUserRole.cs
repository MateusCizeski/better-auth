using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using Domain;

namespace Application.UserRoles
{
    public class AplicUserRole : ApplicationGuid<UserRole, IRepUserRole, UserRoleView>, IAplicUserRole
    {
        private readonly IRepUserRole _repUserRole;
        public AplicUserRole(IUnitOfWork unitOfWork, IRepUserRole repository) : base(unitOfWork, repository)
        {
            _repUserRole = repository;
        }
    }
}
