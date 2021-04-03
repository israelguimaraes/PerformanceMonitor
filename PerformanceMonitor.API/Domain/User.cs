namespace PerformanceMonitor.API.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IPAddress { get; set; }

        public Address Address { get; set; }
        public Company Company { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IPAddress { get; set; }

        public AddressDto Address { get; set; }
        public CompanyDto Company { get; set; }
    }
}
