using ApiBase.Controller.BaseGuid;
using Application.Permissions;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/permissions")]
    public class ControllerPermission : GuidController<IAplicPermission, PermissionView>
    {
        private readonly IAplicPermission _aplicPermission;
        public ControllerPermission(IAplicPermission application) : base(application)
        {
            _aplicPermission = application;
        }

        [HttpPost]
        public IActionResult NewPermission([FromBody] NewPermissionDTO dto)
        {
            try
            {
                var permission = _aplicPermission.NewPermission(dto);

                return RespondSuccess("Permission created with a success.", content: permission);
            }
            catch (Exception e) 
            {
                return RespondError(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePermission([FromRoute] Guid id, [FromBody] UpdatePermissionDTO dto)
        {
            try
            {
                var permission = _aplicPermission.UpdatePermission(id, dto);

                return RespondSuccess("Permission updated with a success.", content: permission);
            }
            catch (Exception e)
            {
                return RespondError(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemovePermission([FromRoute] Guid id)
        {
            try
            {
                _aplicPermission.RemovePermission(id);

                return RespondSuccess("Permission created with a success.");
            }
            catch (Exception e)
            {
                return RespondError(e.Message);
            }
        }
    }
}
