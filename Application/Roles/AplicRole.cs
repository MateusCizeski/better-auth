using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
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
    }
}
