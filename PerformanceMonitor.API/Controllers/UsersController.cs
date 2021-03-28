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

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var time = Stopwatch.StartNew();

            var users = await _userRepository.GetAll();

            var result = users.ToList();

            time.Stop();

            return Ok(new
            {
                UsersCount = result.Count(),
                TotalSeconds = time.ElapsedMilliseconds,
            });
        }
    }
}
