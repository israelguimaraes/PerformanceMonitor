using Microsoft.AspNetCore.Mvc;
using PerformanceMonitor.API.Domain.Repository;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("home")]
        public IActionResult Home()
        {
            return Ok("Home");
        }

        [HttpGet("")]
        public IActionResult Generate()
        {
            var time = Stopwatch.StartNew();

            var users = _userRepository.GetAll().Result;

            time.Stop();

            return Ok(new
            {
                UsersCount = users.Count(),
                TotalSeconds = time.ElapsedMilliseconds,
            });
        }
    }
}
