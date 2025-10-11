using ApiBase.Controller.BaseGuid;
using Application.UserRoles;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/UserRole")]
    public class UserRoleController : GuidController<IAplicUserRole, UserRoleView>
    {
        private readonly IAplicUserRole _aplicUserRole;
        protected UserRoleController(IAplicUserRole application) : base(application)
        {
            _aplicUserRole = application;
        }

        [HttpPost]
        [Route("{userId}/{roleId}")]
        public IActionResult BindUserRole([FromRoute] Guid userId, [FromRoute] Guid roleId)
        {
            try
            {
                _aplicUserRole.BindUserRole(userId, roleId);

                return RespondSuccess("Binding user and role created with success.");
            }
            catch (Exception ex) 
            {
                return RespondError(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoveBindUserRole([FromRoute] Guid id)
        {
            try
            {
                _aplicUserRole.RemoveBindUserRole(id);

                return RespondSuccess("Binding user and role removed with success.");
            }
            catch (Exception ex)
            {
                return RespondError(ex.Message);
            }
        }
    }
}
