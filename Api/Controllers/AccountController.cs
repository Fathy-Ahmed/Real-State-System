using BL.Models;
using BL.Services;
using BL.Utilities;
using DL.Models;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authServices.RegisterAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        //---------------------------------------------------------------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TokenRequstDTO model)
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
        [Authorize(Roles =SD.Manager)]
        public async Task<IActionResult> AddUsertoRole([FromBody] AddRoleDTO model)
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
