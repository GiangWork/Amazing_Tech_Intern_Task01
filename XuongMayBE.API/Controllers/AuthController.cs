using Microsoft.AspNetCore.Mvc;
using XuongMay.ModelViews.AuthModelViews;
using XuongMay.Services.Service;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModelView model)
        {
            await Task.Delay(100);

            if (!_authService.ValidateLoginModel(model, out var errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var user = _authService.AuthenticateUser(model);
            var token = _authService.GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginModelView model)
        {
            if (!_authService.ValidateRegisterModel(model, out var errorMessage))
            {
                return BadRequest(errorMessage);
            }

            try
            {
                _authService.RegisterUser(model);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok("User registered successfully");
        }
    }
}
