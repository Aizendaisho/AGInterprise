using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AGInterprise.WebApi.Models.Requests;
using AGInterprise.WebApi.Services;
using AGInterprise.WebApi.Models.Auth;

namespace AGInterprise.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
            => _authService = authService;

        [HttpPost("register")]
        // [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            await _authService.RegisterAsync(req);
            return Ok("Usuario registrado");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var response = await _authService.LoginAsync(req);
            return Ok(response);
        }
    }
}
