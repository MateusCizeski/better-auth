using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using Domain;

namespace Application.Permissions
{
    public class AplicPermission : ApplicationGuid<Permission, IRepPermission, PermissionView>, IAplicPermission
    {
        private readonly IRepPermission _repPermission;
        public AplicPermission(IUnitOfWork unitOfWork, IRepPermission repository) : base(unitOfWork, repository)
        {
            _repPermission = repository;
        }
    }
}
