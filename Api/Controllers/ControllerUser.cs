using ApiBase.Controller.BaseGuid;
using Application.Users;
using Domain.RefreshTokens;
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

        [HttpPut("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var result = _applicationUser.Login(dto, ipAddress);

                Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                   SameSite = SameSiteMode.Strict,
                   Expires = result.ExpiresAt
                });

                return RespondSuccess("User successfully authenticated.", new
                {
                    Token = result.Token,
                    ExpiresAt = result.ExpiresAt
                });
            }
            catch (Exception e)
            {
                return RespondError(e.Message);
            }
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshRequestDTO dto)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var token = _applicationUser.Refresh(dto.RefreshToken, ipAddress);

                return RespondSuccess("Token refreshed.", token);
            }
            catch (Exception e)
            {
                return RespondError(e.Message);
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] RefreshRequestDTO dto)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                _applicationUser.Logout(dto.RefreshToken, ipAddress);

                return RespondSuccess("User logged out successfully.");
            }
            catch (Exception e)
            {
                return RespondError(e.Message);
            }
        }
    }
}
