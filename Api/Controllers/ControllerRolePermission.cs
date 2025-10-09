using ApiBase.Controller.BaseGuid;
using Application.RolePermissions;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/RolePermission")]
    public class ControllerRolePermission : GuidController<IAplicRolePermission, RolePermissionView>
    {
        private readonly IAplicRolePermission _aplicRolePermission;
        protected ControllerRolePermission(IAplicRolePermission application) : base(application)
        {
            _aplicRolePermission = application;
        }
    }
}
