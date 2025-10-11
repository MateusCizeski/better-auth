using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using ApiBase.Infra.Extensions;
using Domain;

namespace Application.RolePermissions
{
    public class AplicRolePermission : ApplicationGuid<RolePermission, IRepRolePermission, RolePermissionView>, IAplicRolePermission
    {
        private readonly IRepRolePermission _repRolePermission;
        private readonly IMapperRolePermission _mapperRolePermission;
        public AplicRolePermission(IUnitOfWork unitOfWork, 
                                   IRepRolePermission repository,
                                   IMapperRolePermission mapperRolePermission) : base(unitOfWork, repository)
        {
            _repRolePermission = repository;
            _mapperRolePermission = mapperRolePermission;
        }

        public void BindRolePermission(Guid roleId, Guid permissionId)
        {
            var rolePermission = _repRolePermission.Get().Where(p => p.RoleId == roleId && p.PermissionId == permissionId).Any();
            
            if (rolePermission)
            {
                throw new Exception("Role and permission binding already exists.");
            }

            var mapperRolePermission = _mapperRolePermission.NewRolePermission(roleId, permissionId);

            _repRolePermission.Insert(mapperRolePermission);
            Commit();
        }

        public void RemoveBindRolePermission(Guid roleId, Guid permissionId)
        {
            var rolePermission = _repRolePermission.Get().Where(p => p.RoleId == roleId && p.PermissionId == permissionId).FirstOrDefault().uExceptionSeNull("Role and permission binding not exists.");

            _repRolePermission.Remove(rolePermission);
            Commit();
        }
    }
}
