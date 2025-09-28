using ApiBase.Repository.Repositorys;
using Domain;

namespace Repository.RefreshTokens
{
    public class RepRefreshToken : RepositoryBase<RefreshToken>, IRepRefreshToken
    {
        private readonly ContextDataBase _contextDataBase;
        public RepRefreshToken(ContextDataBase context) : base(context)
        {
            _contextDataBase = context;
        }
    }
}
