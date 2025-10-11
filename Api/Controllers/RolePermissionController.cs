using ApiBase.Controller.BaseGuid;
using Application.RolePermissions;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/RolePermission")]
    public class RolePermissionController : GuidController<IAplicRolePermission, RolePermissionView>
    {
        private readonly IAplicRolePermission _aplicRolePermission;
        public RolePermissionController(IAplicRolePermission application) : base(application)
        {
            _aplicRolePermission = application;
        }

        [HttpPost]
        [Route("{roleId}/{permissionId}")]
        public IActionResult BindRolePermission([FromRoute] Guid roleId, [FromRoute] Guid permissionId)
        {
            try
            {
                _aplicRolePermission.BindRolePermission(roleId, permissionId);

                return RespondSuccess("Binding role and permission created with a succes.");
            }
            catch (Exception ex) 
            {
                return RespondError(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoveBindingRolePermission([FromRoute] Guid id)
        {
            try
            {
                _aplicRolePermission.RemoveBindRolePermission(id);

                return RespondSuccess("Binding role and permission removed with a succes.");
            }
            catch (Exception ex)
            {
                return RespondError(ex.Message);
            }
        }
    }
}
