using PerformanceMonitor.API.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceMonitor.API.Domain.Repository
{
    public class FakeUserRepository : IUserRepository
    {
        private const int Count = 300_000;

        public Task<IEnumerable<User>> GetAll()
        {
            var users = FakeUserGenerator.FakeData().Generate(Count);

            return Task.FromResult(users.AsEnumerable());
        }
    }
}
