using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using ApiBase.Infra.Extensions;
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

        public PermissionView NewPermission(NewPermissionDTO dto)
        {
            var permission = _mapperPermission.NewPermission(dto);

            _repPermission.Insert(permission);
            Commit();

            return _mapperPermission.ToView(permission);
        }

        public PermissionView UpdatePermission(Guid id, UpdatePermissionDTO dto)
        {
            var permission = _repPermission.GetById(id).uExceptionSeNull("Permission not found.");

            _mapperPermission.UpdatePermission(permission, dto);
            Commit();

            return _mapperPermission.ToView(permission);
        }

        public void RemovePermission(Guid id)
        {
            var permission = _repPermission.GetById(id);

            _repPermission.Remove(permission);
            Commit();
        }
    }
}
