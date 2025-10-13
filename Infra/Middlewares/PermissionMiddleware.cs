using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Infra.Middlewares
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;
        public PermissionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var requiredPermission = endpoint?.Metadata.GetMetadata<RequirePermissionAttribute>();

            if (requiredPermission != null)
            {
                var userPermissions = context.User.Claims
                    .Where(c => c.Type == "permission")
                    .Select(c => c.Value);

                if (!userPermissions.Contains(requiredPermission.Permission))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden: Missing permission.");
                    return;
                }
            }

            await _next(context);
        }
    }

    public class RequirePermissionAttribute : AuthorizeAttribute
    {
        public string Permission { get; }
        public RequirePermissionAttribute(string permission) => Permission = permission;
    }
}
