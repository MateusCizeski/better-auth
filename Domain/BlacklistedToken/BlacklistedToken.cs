using ApiBase.Domain.Entities;

namespace Domain.BlacklistedTokens
{
    public class BlacklistedToken : EntityGuid
    {
        public string Token { get; set; }
        public DateTime RevokedAt { get; set; }
        public Guid UserId { get; set; }
        public Domain.Users.User User { get; set; }
    }
}
