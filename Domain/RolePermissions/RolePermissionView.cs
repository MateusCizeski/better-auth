using ApiBase.Domain.View;

namespace Domain
{
    public class RolePermissionView : IdGuidView
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public DateTime CreatedAt { get; set; }

        public RoleView Role { get; set; }
        public PermissionView Permission { get; set; }
    }
}
