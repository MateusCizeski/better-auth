using ApiBase.Controller.BaseGuid;
using Application.UserRoles;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/UserRole")]
    public class ControllerUserRole : GuidController<IAplicUserRole, UserRoleView>
    {
        private readonly IAplicUserRole _aplicUserRole;
        protected ControllerUserRole(IAplicUserRole application) : base(application)
        {
            _aplicUserRole = application;
        }
    }
}
