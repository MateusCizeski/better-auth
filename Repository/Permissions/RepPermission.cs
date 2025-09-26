using ApiBase.Repository.Repositorys;
using Domain;

namespace Repository.Permissions
{
    public class RepPermission : RepositoryBase<Permission>, IRepPermission
    {
        public RepPermission(ContextDataBase context) : base(context) { }
    }
}
