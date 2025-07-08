using Domain.User;
using Microsoft.Extensions.Configuration;

namespace CrossCutting.JWT
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
