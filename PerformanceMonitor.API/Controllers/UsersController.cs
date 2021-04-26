using Microsoft.AspNetCore.Mvc;
using PerformanceMonitor.API.Domain;
using PerformanceMonitor.API.Domain.Repository;
using PerformanceMonitor.API.Services;
using System;
using System.Collections.Generic;
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

        private readonly int Count = 1_000_000;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("home")]
        public IActionResult Home()
        {
            return Ok(new 
            {
                Title = "Home",
                DotnetMonitor.ProcessID
            });
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            DotnetMonitor.InitializeCollector();

            var time = Stopwatch.StartNew();
            var users = await _userRepository.GetAll(Count);
            var dtos = Map(users);
            time.Stop();

            DotnetMonitor.StopCollector();

            return Ok(new
            {
                UsersCount = dtos.Count(),
                TotalSeconds = TimeSpan.FromMilliseconds(time.ElapsedMilliseconds).TotalSeconds,
                DotnetMonitor.ProcessID
            });
        }

        private IEnumerable<UserDto> Map(IEnumerable<User> users)
        {
            var result = new List<UserDto>(users.Count());

            UserDto dto = null;
            foreach (var user in users)
            {
                dto = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    IPAddress = user.IPAddress,
                    Address = new AddressDto
                    {
                        Street = user.Address.Street,
                        Country = user.Address.Country,
                        City = user.Address.City,
                        ZipCode = user.Address.ZipCode,
                        Geo = new GeoDto
                        {
                            Lat = user.Address.Geo.Lat,
                            Lng = user.Address.Geo.Lng,
                        }
                    },
                    Company = new CompanyDto
                    {
                        Name = user.Company.Name,
                        City = user.Company.City,
                        Country = user.Company.Country,
                    }
                };

                result.Add(dto);
            }

            return result;
        }
    }
}
