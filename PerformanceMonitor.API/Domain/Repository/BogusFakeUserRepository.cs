using PerformanceMonitor.API.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceMonitor.API.Domain.Repository
{
    public class BogusFakeUserRepository : IUserRepository
    {
        public Task<IEnumerable<User>> GetAll(int count)
        {
            var users = FakeUserGenerator.FakeData().Generate(count);

            return Task.FromResult(users.AsEnumerable());
        }
    }
}
