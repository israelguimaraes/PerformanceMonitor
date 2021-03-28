using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceMonitor.API.Domain.Repository
{
    public class FakeUserRepository : IUserRepository
    {
        public Task<IEnumerable<User>> GetAll()
        {
            var users = new List<User>();

            var user = new User
            {
                Id = 1,
                Name = "Israel Guimarães",
                Username = "israelguimaraes",
                Email = "israel@email.com",
                Phone = "1-770-736-8031 x56442",
                Website = "http://www.github.com/israelguimaraes",
                Address = new Address
                {
                    Street = "Xpto Street",
                    Suite = "Apt. 556",
                    City = "Gwenborough",
                    Zipcode = "92998-3874",
                    Geo = new Geo
                    {
                        Lat = "-37.3159",
                        Lng = "81.1496",
                    }
                },
                Company = new Company
                {
                    Name = "Romaguera-Crona",
                    CatchPhrase = "ulti-layered client-server neural-net",
                    Bs = "harness real-time e-markets"
                }
            };

            users.Add(user);

            var result = Task.FromResult(users.AsEnumerable());

            return result;
        }
    }
}
