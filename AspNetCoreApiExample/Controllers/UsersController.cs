using AspNetCoreApiExample.Business;
using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AspNetCoreApiExample.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/login")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserBusiness _userBusiness;

        public UsersController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        // GET api/values
        [HttpPost]
        [SwaggerResponse(200, Type = typeof(UserVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [AllowAnonymous]
        public ActionResult<object> Post([FromBody] UserVO user)
        {
            if (user == null) return BadRequest();
            return Ok(_userBusiness.FindByLogin(user));
        }

    }
}