using ApiBase.Repository.Contexts;
using ApiBase.Repository.Repositorys;
using Domain;

namespace Repository.UserRoles
{
    public class RepUserRole : RepositoryBase<UserRole>, IRepUserRole
    {
        public RepUserRole(Context context) : base(context) { }
    }
}
