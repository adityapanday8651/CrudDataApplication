using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly ILoggerRepository<AuthController> _loggerRepository;

        public AuthController(IJwtService jwtService, ILoggerRepository<AuthController> loggerRepository)
        {
            _jwtService = jwtService;
            _loggerRepository = loggerRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModelDto user)
        {
            if (user.Username == "admin" && user.Password == "password")
            {
                var token = _jwtService.GenerateToken(user.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var userName = User.Identity?.Name;
            _loggerRepository.InfoWithObjectMessage($"User {userName} logged out at {DateTime.UtcNow}");
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
