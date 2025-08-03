using ApiBase.Core.Repositories.Repositories.Repository;
using Domain.Users;

namespace Repository.Users
{
    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        public RepositoryUser(ContextDataBase context) : base(context)
        {
        }
    }
}
