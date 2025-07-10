using Application.User.DTOs;
using Infrastructure.Repositories;

namespace Application.User
{
    public class AplicUser : IAplicUser
    {
        private readonly IRepUser _repUser;

        public AplicUser(RepUser repUser)
        {
            _repUser = repUser;
        }

        public void CreateUser(CreateUserDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
