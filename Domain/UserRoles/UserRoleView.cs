using ApiBase.Domain.View;

namespace Domain
{
    public class UserRoleView : IdGuidView
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserView User { get; set; }
        public RoleView Role { get; set; }
    }
}
