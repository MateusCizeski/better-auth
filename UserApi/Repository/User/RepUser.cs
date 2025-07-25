using Base.Repository;
using Domain.User;
using Infrastructure.Data;

namespace Repository
{
    public class RepUser : RepositoryBase<User>, IRepUser
    {
        public RepUser(ApplicationDbContext context) : base(context)
        {
        }
    }
}
