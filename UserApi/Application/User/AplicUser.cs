using Domain.User.DTOs;
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

        public UserDetailDTO CreateUser(CreateUserDTO dto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = _mapperUser.NewUser(dto, passwordHash);

            return _repUser.CreateUser(user);
        }

        public UserDetailDTO DetailUser(Guid id)
        {
            return _repUser.DetailUser(id);
        }

        public UserDetailDTO UpdateUser(Guid id, UpdateUserDTO dto)
        {
            var user = _repUser.SearchUserById(id);
            _mapperUser.UpdateUser(user, dto);

           return _repUser.UpdateUser(user);
        }
    }
}
