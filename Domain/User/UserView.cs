using ApiBase.Domain.View;

namespace Domain.Users
{
    public class UserView : IdGuidView
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
