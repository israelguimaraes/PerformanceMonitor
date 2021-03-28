using Microsoft.AspNetCore.Mvc;
using PerformanceMonitor.API.Domain.Repository;
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
            var users = await _userRepository.GetAll();

            return Ok(users);
        }
    }
}
