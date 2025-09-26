using ApiBase.Domain.View;

namespace Domain
{
    public class BlacklistedTokenView : IdGuidView
    {
        public string Token { get; set; }
        public DateTime RevokedAt { get; set; }
        public Guid UserId { get; set; }
        public UserView User { get; set; }
    }
}
