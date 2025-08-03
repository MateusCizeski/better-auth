using ApiBase.Core.Application.ApplicationGuid;
using Domain.Users;

namespace Application.Users
{
    public interface IApplicationUser : IApplicationGuid<UserView>
    {
    }
}
