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

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeToken([FromBody] string token)
        {
            await _authService.RevokeTokenAsync(token);
            return Ok("Token revoked.");
        }
    }
}
