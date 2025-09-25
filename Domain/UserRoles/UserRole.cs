using ApiBase.Domain.Entities;

namespace Domain
{
    public class UserRole : EntityGuid
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
