using ApiBase.Domain.Entities;

namespace Domain
{
    public class Role : EntityGuid
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<UserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}
