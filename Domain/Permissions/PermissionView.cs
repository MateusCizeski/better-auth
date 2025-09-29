using ApiBase.Domain.View;

namespace Domain
{
    public class PermissionView : IdGuidView
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<RolePermissionView> RolePermissions { get; set; }
    }
}
