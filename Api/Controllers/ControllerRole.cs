using ApiBase.Controller.BaseGuid;
using Application.Roles;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/roles")]
    public class ControllerRole : GuidController<IAplicRole, RoleView>
    {
        private readonly IAplicRole _aplicRole;
        public ControllerRole(IAplicRole application) : base(application)
        {
            _aplicRole = application;
        }

        [HttpPost]
        public IActionResult NewUser([FromBody] NewRoleDTO dto)
        {
            try
            {
                var role = _aplicRole.NewRole(dto);

                return RespondSuccess(message: "Role created with a success.", content: role);
            }
            catch (Exception e)
            {
                return RespondError(e.Message);
            }
        }
    }
}
