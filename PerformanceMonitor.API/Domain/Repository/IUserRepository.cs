using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceMonitor.API.Domain.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
    }
}
