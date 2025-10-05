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
    }
}
