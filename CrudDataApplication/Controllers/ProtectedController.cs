using Microsoft.AspNetCore.Mvc;

namespace CrudDataApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtectedController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var username = User.Identity.Name;
            return Ok($"Hello, {username}! This is protected data.");
        }
    }
}
