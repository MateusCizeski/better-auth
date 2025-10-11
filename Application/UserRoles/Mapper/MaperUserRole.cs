using Domain;

namespace Application.UserRoles
{
    public class MaperUserRole : IMapperUserRole
    {
        public UserRole NewUserRole(Guid userId, Guid roleId)
        {
            return new UserRole
            {
                RoleId = roleId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}
