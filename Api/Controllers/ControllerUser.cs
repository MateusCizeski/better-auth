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
                var userAgent = Request.Headers["User-Agent"].ToString();
                var deviceId = Request.Headers["X-Device-Id"].ToString();

                var result = _applicationUser.Login(dto, ipAddress, deviceId, userAgent);

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
                var refreshToken = Request.Cookies["refreshToken"] ?? dto.RefreshToken;
                var userAgent = Request.Headers["User-Agent"].ToString();
                var deviceId = Request.Headers["X-Device-Id"].ToString();

                var result = _applicationUser.Refresh(refreshToken, ipAddress, userAgent, deviceId);

                Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.ExpiresAt
                });

                return RespondSuccess("Token refreshed.", new
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

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] RefreshRequestDTO dto)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

                var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var accessToken = authHeader.Replace("Bearer ", "");

                _applicationUser.Logout(dto.RefreshToken, ipAddress, accessToken);

                Response.Cookies.Delete("refreshToken");

                return RespondSuccess("User logged out successfully.");
            }
            catch (Exception e)
            {
                return RespondError(e.Message);
            }
        }

        [HttpGet("sessions")]
        public IActionResult GetSessions()
        {
            try
            {
                var userId = User.Claims.First(c => c.Type == "id").Value;
                var sessions = _applicationUser.GetSessions(Guid.Parse(userId));

                return RespondSuccess("Active sessions.", sessions);
            }
            catch (Exception e)
            {
                return RespondError(e.Message);
            }
        }

        [HttpDelete("sessions/{deviceId}")]
        public IActionResult RevokeSession([FromRoute] string deviceId)
        {
            try
            {
                var userId = User.Claims.First(c => c.Type == "id").Value;
                _applicationUser.RevokeSession(Guid.Parse(userId), deviceId);

                return RespondSuccess("Session revoked.");
            }
            catch(Exception e)
            {
                return RespondError(e.Message);
            }
        }
    }
}
