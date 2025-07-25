using Base.Application;
using Base.Controller;
using Domain.User;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : BaseControllerGuid<User>
    {
        public UserController(IApplicationBase<User> applicationBase) : base(applicationBase)
        {
        }



        //[HttpGet("{id}")]
        //public IActionResult DetailUser([FromRoute] Guid id)
        //{
        //    try
        //    {
        //        var user = _aplicUser.DetailUser(id);

        //        return Ok(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPut("{id}")]
        //public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDTO dto)
        //{
        //    try
        //    {
        //        _aplicUser.UpdateUser(id, dto);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet]
        //public IActionResult AuthUser([FromBody] AuthUserDTO dto)
        //{
        //    try
        //    {
        //        _aplicUser.AuthUser(dto);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
