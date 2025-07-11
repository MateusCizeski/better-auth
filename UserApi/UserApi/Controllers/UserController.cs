using Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IAplicUser _aplicUser;

        public UserController(IAplicUser aplicUser)
        {
            _aplicUser = aplicUser;
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserDTO dto)
        {
            try
            {
                _aplicUser.CreateUser(dto);

                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
