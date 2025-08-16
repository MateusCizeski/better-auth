using ApiBase.Api.BaseGuid;
using Application.Users;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class ControllerUser : ControllerGuid<IApplicationUser, UserView>
    {
        public ControllerUser(IApplicationUser application) : base(application)       {
        }
    }
}
