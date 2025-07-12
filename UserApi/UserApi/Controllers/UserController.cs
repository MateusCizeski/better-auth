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
        public IActionResult CreateUser([FromBody] CreateUserDTO dto)
        {
            try
            {
                var user = _aplicUser.CreateUser(dto);

                return Ok(user);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult DetailUser([FromRoute] Guid id)
        {
            try
            {
                var user = _aplicUser.DetailUser(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
