using ApiBase.Api.BaseGuid;
using Application.Users;
using Domain.User.DTOs;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class ControllerUser : ControllerGuid<IApplicationUser, UserView>
    {
        private readonly IApplicationUser _applicationUser;
        public ControllerUser(IApplicationUser application) : base(application)
        {
            _applicationUser = application;
        }

        [HttpPost]
        public IActionResult NewUser([FromBody] NewUserDTO dto)
        {
            try
            {
                var user = _applicationUser.NewUser(dto);

                return RespondSuccess(message: "User created with a success.", content: user);
            }
            catch(Exception e)
            {
                return RespondError(e.Message);
            }
        }
    }
}
