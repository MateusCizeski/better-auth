using ApiBase.Core.Application.ApplicationGuid;
using ApiBase.Core.Domain.Interfaces;
using Domain.User.DTOs;
using Domain.Users;

namespace Application.Users
{
    public class ApplicationUser : ApplicationGuid<User, IRepositoryUser, UserView>, IApplicationUser
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly IMapperUser _mapperUser;
        public ApplicationUser(IUnitOfWork unitOfWork, 
                               IRepositoryUser repository, 
                               IMapperUser mapperUser) : base(unitOfWork, repository)
        {
            _repositoryUser = repository;
            _mapperUser = mapperUser;
        }

        public UserView NewUser(NewUserDTO dto)
        {
            var user = _mapperUser.NewUser(dto);

            _repositoryUser.Insert(user);
            Commit();

            return _mapperUser.ToView(user);
        }
    }
}
