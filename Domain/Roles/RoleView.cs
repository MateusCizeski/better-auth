using ApiBase.Domain.View;

namespace Domain
{
    public class RoleView : IdGuidView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<UserRoleView> UserRoles { get; set; }
        public List<RolePermissionView> RolePermissions { get; set; }
    }
}
