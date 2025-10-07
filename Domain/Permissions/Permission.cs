using ApiBase.Domain.Entities;

namespace Domain
{
    public class Permission : EntityGuid
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
