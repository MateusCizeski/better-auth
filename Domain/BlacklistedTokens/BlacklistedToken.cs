using ApiBase.Domain.Entities;

namespace Domain
{
    public class BlacklistedToken : EntityGuid
    {
        public string Token { get; set; }
        public DateTime RevokedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
