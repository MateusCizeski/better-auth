using ApiBase.Repository.Contexts;
using ApiBase.Repository.Repositorys;
using Domain.RefreshTokens;

namespace Repository.RefreshTokens
{
    public class RepRefreshToken : RepositoryBase<RefreshToken>
    {
        private readonly ContextDataBase _contextDataBase;
        public RepRefreshToken(ContextDataBase context) : base(context)
        {
            _contextDataBase = context;
        }
    }
}
