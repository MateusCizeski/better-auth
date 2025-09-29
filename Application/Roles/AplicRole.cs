using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using Domain;

namespace Application.Roles
{
    public class AplicRole : ApplicationGuid<Role, IRepRole, RoleView>, IAplicRole
    {
        private readonly IRepRole _repRole;
        public AplicRole(IUnitOfWork unitOfWork, IRepRole repository) : base(unitOfWork, repository)
        {
            _repRole = repository;
        }
    }
}
