using ApiBase.Domain.View;

namespace Domain
{
    public class UserView : IdGuidView
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
