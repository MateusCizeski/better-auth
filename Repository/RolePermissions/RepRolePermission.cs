using ApiBase.Repository.Contexts;
using ApiBase.Repository.Repositorys;
using Domain;

namespace Repository.RolePermissions
{
    public class RepRolePermission : RepositoryBase<RolePermission>, IRepRolePermission
    {
        public RepRolePermission(ContextDataBase context) : base(context) { }
    }
}
