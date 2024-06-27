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
        private readonly IRegisterService _registerService;
        private readonly ILoggerRepository<AuthController> _loggerRepository;

        public AuthController(IJwtService jwtService, ILoggerRepository<AuthController> loggerRepository, IRegisterService registerService)
        {
            _jwtService = jwtService;
            _loggerRepository = loggerRepository;
            _registerService = registerService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            try
            {
                await _registerService.AddRegisterAsync(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModelDto user)
        {
            try
            {
                var registeredUser = await _registerService.FindByNameAsync(user.Username);

                if (registeredUser != null && BCrypt.Net.BCrypt.Verify(user.Password, registeredUser.Password))
                {
                    var token = _jwtService.GenerateToken(user.Username, registeredUser.RoleName);
                    return Ok(new { Token = token });
                }

                    return Unauthorized();
            }catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
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
