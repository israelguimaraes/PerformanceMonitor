using Bogus;
using PerformanceMonitor.API.Domain;

namespace PerformanceMonitor.API.Data
{
    public class FakeUserGenerator
    {
        public static Faker<User> FakeData()
        {
            var usersIds = 1;

            var fakeGeo = new Faker<Geo>()
                .RuleFor(p => p.Lat, f => f.Address.Latitude())
                .RuleFor(p => p.Lng, f => f.Address.Longitude());

            var fakeAddress = new Faker<Address>()
                .RuleFor(o => o.Street, f => f.Address.StreetName())
                .RuleFor(o => o.Country, f => f.Address.Country())
                .RuleFor(o => o.City, f => f.Address.City())
                .RuleFor(o => o.ZipCode, f => f.Address.ZipCode())
                .RuleFor(o => o.Geo, f => fakeGeo.Generate());

            var fakeCompany = new Faker<Company>()
                .RuleFor(p => p.Name, f => f.Company.CompanyName())
                .RuleFor(o => o.Country, f => f.Address.Country())
                .RuleFor(o => o.City, f => f.Address.City());

            var fakeUser = new Faker<User>()
                .RuleFor(p => p.Id, f => usersIds++)
                .RuleFor(p => p.Name, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName())
                .RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.Name, p.LastName))
                .RuleFor(p => p.Phone, f => f.Phone.PhoneNumber("(###)-###-####"))
                .RuleFor(p => p.IPAddress, (f, p) => f.Internet.Ip())
                .RuleFor(p => p.Address, f => fakeAddress.Generate())
                .RuleFor(p => p.Company, f => fakeCompany.Generate());

            return fakeUser;
        }
    }
}
