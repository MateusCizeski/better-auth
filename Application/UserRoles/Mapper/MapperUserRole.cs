using Domain;

namespace Application.UserRoles
{
    public class MapperUserRole : IMapperUserRole
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
