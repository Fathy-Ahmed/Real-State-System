using BL.Interfaces;
using BL.Models;
using DL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly IAuthServices _authServices;

        public AccountController
            (UserManager<ApplicationUser> userManager, IConfiguration config, IAuthServices authServices)
        {
            this.userManager = userManager;
            this.config = config;
            this._authServices = authServices;
        }
        //---------------------------------------------------------------------------

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authServices.RegisterAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        //---------------------------------------------------------------------------
        [HttpPost("login2")]
        public async Task<IActionResult> Login2([FromBody] TokenRequstModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authServices.GetTokenAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        //---------------------------------------------------------------------------
        [HttpPost("AddUsertoRole")]
        public async Task<IActionResult> AddUsertoRole([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authServices.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
        //---------------------------------------------------------------------------


    }
}
