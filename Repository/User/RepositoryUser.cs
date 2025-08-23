using ApiBase.Repository.Repositorys;
using Domain.Users;

namespace Repository.Users
{
    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        private readonly ContextDataBase _contextDataBase;
        public RepositoryUser(ContextDataBase context) : base(context)
        {
            _contextDataBase = context;
        }
    }
}
