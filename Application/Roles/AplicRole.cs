using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using ApiBase.Infra.Extensions;
using Domain;

namespace Application.Roles
{
    public class AplicRole : ApplicationGuid<Role, IRepRole, RoleView>, IAplicRole
    {
        private readonly IRepRole _repRole;
        private readonly IMapperRole _mapperRole;
        public AplicRole(IUnitOfWork unitOfWork, 
                         IRepRole repository, 
                         IMapperRole mapperRole) : base(unitOfWork, repository)
        {
            _repRole = repository;
            _mapperRole = mapperRole;
        }

        public RoleView NewRole(NewRoleDTO dto)
        {
            var role = _mapperRole.NewRole(dto);

            _repRole.Insert(role);
            Commit();

            return _mapperRole.ToView(role);
        }

        public RoleView UpdateRole(Guid id, UpdateRoleDTO dto)
        {
            var role = _repRole.GetById(id).uExceptionSeNull("Role not found.");

            _mapperRole.UpdateUser(role, dto);
            Commit();

            return _mapperRole.ToView(role);
        }

        public void RemoveRole(Guid id)
        {
            var role = _repRole.GetById(id).uExceptionSeNull("Role not exists.");

            _repRole.Remove(role);
            Commit();
        }
    }
}
