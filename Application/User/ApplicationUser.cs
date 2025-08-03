using ApiBase.Core.Application.ApplicationGuid;
using ApiBase.Core.Domain.Interfaces;
using Domain.Users;

namespace Application.Users
{
    public class ApplicationUser : ApplicationGuid<User, IRepositoryUser, UserView>, IApplicationUser
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryUser _repositoryUser;
        public ApplicationUser(IUnitOfWork unitOfWork, IRepositoryUser repository) : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repositoryUser = repository;
        }
    }
}
