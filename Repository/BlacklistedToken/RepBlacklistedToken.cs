using ApiBase.Repository.Contexts;
using ApiBase.Repository.Repositorys;
using Domain.BlacklistedTokens;

namespace Repository.BlacklistedTokens
{
    public class RepBlacklistedToken : RepositoryBase<BlacklistedToken>, IRepBlacklistedToken
    {
        public RepBlacklistedToken(ContextDataBase context) : base(context) { }
    }
}
