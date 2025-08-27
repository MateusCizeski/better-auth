using ApiBase.Application.ApplicationGuid;
using Domain.RefreshTokens;

namespace Application.RefreshTokens
{
    public interface IAplicRefreshToken : IApplicationGuid<RefreshTokenView>
    {
    }
}
