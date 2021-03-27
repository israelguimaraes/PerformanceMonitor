using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PerformanceMonitor.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class UsersController : Controller
    {
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }
    }
}
