using ApiBase.Repository.Contexts;
using ApiBase.Repository.Repositorys;
using Domain;

namespace Repository.Roles
{
    public class RepRole : RepositoryBase<Role>, IRepRole
    {
        public RepRole(Context context) : base(context) { }
    }
}
