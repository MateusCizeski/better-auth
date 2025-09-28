using ApiBase.Application.ApplicationGuid;
using ApiBase.Domain.Interfaces;
using Domain;

namespace Application.RefreshTokens
{
    public class AplicRefreshToken : ApplicationGuid<RefreshToken, IRepRefreshToken, RefreshTokenView>, IAplicRefreshToken
    {
        private readonly IRepRefreshToken _repRefreshToken;
        public AplicRefreshToken(IUnitOfWork unitOfWork, 
                                 IRepRefreshToken repository) : base(unitOfWork, repository)
        {
            _repRefreshToken = repository;
        }
    }
}
