using ApiBase.Domain.Entities;

namespace Domain
{
    public class RolePermission : EntityGuid
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
