using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using Domain;

namespace Application.Permissions
{
    public class AplicPermission : ApplicationGuid<Permission, IRepPermission, PermissionView>, IAplicPermission
    {
        private readonly IRepPermission _repPermission;
        private readonly IMapperPermission _mapperPermission;
        public AplicPermission(IUnitOfWork unitOfWork, 
                               IRepPermission repository, 
                               IMapperPermission mapperPermission) : base(unitOfWork, repository)
        {
            _repPermission = repository;
            _mapperPermission = mapperPermission;
        }
    }
}
