using Microsoft.AspNetCore.Mvc;

namespace Sistema.Controllers
{
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet("/Verify")]
        public IActionResult Index()
        {
            return Ok(value: "Sitio (disponible).......");
        }
    }
}
