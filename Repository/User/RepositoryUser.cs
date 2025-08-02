using ApiBase.Core.Repositories.Contexts;
using ApiBase.Core.Repositories.Repositories.Repository;
using Domain.Users;

namespace Repository.Users
{
    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        public RepositoryUser(Context context) : base(context)
        {
        }
    }
}
