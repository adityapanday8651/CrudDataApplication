using CrudDataApplication.Dto;
using CrudDataApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IRegisterService _registerService;
        private readonly IConfiguration _configuration;
        private readonly ILoggerRepository<AuthController> _loggerRepository;

        public AuthController(IJwtService jwtService, ILoggerRepository<AuthController> loggerRepository, IRegisterService registerService, IConfiguration configuration)
        {
            _jwtService = jwtService;
            _loggerRepository = loggerRepository;
            _registerService = registerService;
            _configuration = configuration;
        }


        [HttpPost("AddRegisterAsync")]
        public async Task<IActionResult> AddRegisterAsync([FromBody] RegisterDto model)
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
                    var jwtToken = _jwtService.GenerateToken(user.Username, registeredUser.RoleName);
                    var refreshToken = await _jwtService.GenerateRefreshTokenAsync(user.Username);
                    return Ok(new
                    {
                        Token = jwtToken,
                        RefreshToken = refreshToken.Token
                    });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDto model)
        {
            var validatedToken = GetPrincipalFromExpiredToken(model.Token);
            if (validatedToken == null)
            {
                return BadRequest("Invalid token.");
            }

            var userName = validatedToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            var refreshToken = await _jwtService.ValidateRefreshTokenAsync(model.RefreshToken);
            if (refreshToken == null)
            {
                return BadRequest("Invalid refresh token.");
            }

            var newJwtToken = _jwtService.GenerateToken(userName, validatedToken.Claims.First(x => x.Type == ClaimTypes.Role).Value);
            var newRefreshToken = await _jwtService.GenerateRefreshTokenAsync(userName);

            return Ok(new
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken.Token
            });
        }



        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var userName = User.Identity?.Name;
            _loggerRepository.InfoWithObjectMessage($"User {userName} logged out at {DateTime.UtcNow}");
            return Ok(new { message = "Logged out successfully" });
        }


        [HttpGet("GetAllRolesAsync")]
        public async Task<ActionResult<ResponseModelDto>> GetAllRolesAsync()
        {
            try
            {
                var lsrRolesData = await _registerService.GetAllRolesAsync();
                return Ok(lsrRolesData);
            }
            catch (Exception ex)
            {
                _loggerRepository.ErrorMessage(ex);
                return BadRequest(ex.Message);
            }
        }


        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }

            return null;
        }
    }
}
