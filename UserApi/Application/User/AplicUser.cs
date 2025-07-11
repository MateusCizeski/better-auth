using Infrastructure.Repositories;

namespace Application.Users
{
    public class AplicUser : IAplicUser
    {
        private readonly IRepUser _repUser;
        private readonly IMapperUser _mapperUser;

        public AplicUser(RepUser repUser, IMapperUser mapperUser)
        {
            _repUser = repUser;
            _mapperUser = mapperUser;
        }

        public void CreateUser(CreateUserDTO dto)
        {
            var user = _mapperUser.NewUser(dto);

            _repUser.AddAsync(user);
        }
    }
}
