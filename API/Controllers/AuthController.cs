using Microsoft.AspNetCore.Mvc;
using Services.IService;
using Services.DTO;

namespace API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _authService.LoginAsync(request);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials.");
            }
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (result == "User already exists")
            {
                return BadRequest(new { Message = result });
            }
            return Ok(new { Token = result });
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeToken([FromBody] string token)
        {
            await _authService.RevokeTokenAsync(token);
            return Ok("Token revoked.");
        }
    }
}
