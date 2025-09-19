using ApiBase.Domain.View;

namespace Domain.BlacklistedToken
{
    public class BlacklistedTokenView : IdGuidView
    {
        public string Token { get; set; }
        public DateTime RevokedAt { get; set; }
        public Guid UserId { get; set; }
        public Domain.Users.UserView User { get; set; }
    }
}
