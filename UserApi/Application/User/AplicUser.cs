using Base.Application;
using Base.Repository;
using Domain.User;
using Infrastructure.Data;

namespace Application.Users
{
    public class AplicUser : ApplicationBase<User>, IAplicUser
    {
        public AplicUser(IRepositoryBase<User> repository, ApplicationDbContext dbContext) : base(repository, dbContext)
        {
        }
    }
}
