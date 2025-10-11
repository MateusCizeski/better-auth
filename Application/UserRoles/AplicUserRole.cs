using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using ApiBase.Infra.Extensions;
using Domain;

namespace Application.UserRoles
{
    public class AplicUserRole : ApplicationGuid<UserRole, IRepUserRole, UserRoleView>, IAplicUserRole
    {
        private readonly IRepUserRole _repUserRole;
        private readonly IMapperUserRole _mapperUserRole;
        public AplicUserRole(IUnitOfWork unitOfWork, IRepUserRole repository, IMapperUserRole mapperUserRole) : base(unitOfWork, repository)
        {
            _repUserRole = repository;
            _mapperUserRole = mapperUserRole;
        }

        public void BindUserRole(Guid userId, Guid roleId)
        {
            var userRole = _repUserRole.Get().Where(p => p.UserId == userId && p.RoleId == roleId).Any();

            if (userRole)
            {
                throw new Exception("Role and user binding already exists.");
            }

            var mapperUserRole = _mapperUserRole.NewUserRole(userId, roleId);

            _repUserRole.Insert(mapperUserRole);
            Commit();
        }

        public void RemoveBindUserRole(Guid id)
        {
            var userRole = _repUserRole.Get().Where(p => p.Id == id).FirstOrDefault().uExceptionSeNull("Biding user and role not exists.");
        
            _repUserRole.Remove(userRole);
            Commit();
        }
    }
}
