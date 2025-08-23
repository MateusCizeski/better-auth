using ApiBase.Controller.BaseGuid;
using Application.Users;
using Domain.User.DTOs;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class ControllerUser : GuidController<IApplicationUser, UserView>
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
            catch (Exception e)
            {
                return RespondError(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateSelfDto dto)
        {
            try
            {
                var user = _applicationUser.UpdateUser(id, dto);

                return RespondSuccess(message: "User created with a success.", content: user);
            }
            catch (Exception e)
            {
                return RespondError(e.Message);
            }
        }

        [HttpPut]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            try
            {
                var token = _applicationUser.Login(dto);

                return RespondSuccess(message: "\r\nuser successfully authenticated.", content: token);
            }
            catch (Exception e)
            {
                return RespondError(e.Message);
            }

        }
    }
}
