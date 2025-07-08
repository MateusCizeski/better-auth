using Domain.User;

namespace CrossCutting.JWT
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
