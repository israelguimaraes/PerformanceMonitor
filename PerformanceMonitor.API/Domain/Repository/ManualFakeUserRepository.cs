using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceMonitor.API.Domain.Repository
{
    public class ManualFakeUserRepository : IUserRepository
    {
        public Task<IEnumerable<User>> GetAll(int count)
        {
            var users = new List<User>();

            for (int i = 0; i < count; i++)
            {
                var user = new User
                {
                    Id = i,
                    Name = $"User {i}",
                    LastName = $"Last Name {i}",
                    Email = $"email_{i}@email.com",
                    Phone = $"1-770-736-{i}",
                    IPAddress = Guid.NewGuid().ToString(),
                    Address = new Address
                    {
                        Street = $"Street {i}",
                        Country = $"Country {i}",
                        City = $"City {i}",
                        ZipCode = $"92998-{i}",
                        Geo = new Geo
                        {
                            Lat = (i + 1) * 2 + i,
                            Lng = (i + 1) * 2 + i,
                        }
                    },
                    Company = new Company
                    {
                        Name = $"Company {i}",
                        City = $"City {i}",
                        Country = $"Country {i}"
                    }
                };

                users.Add(user);
            }

            var result = Task.FromResult(users.AsEnumerable());

            return result;
        }
    }
}
