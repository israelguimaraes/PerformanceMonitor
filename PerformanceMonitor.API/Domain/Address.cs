namespace PerformanceMonitor.API.Domain
{
    public class Address
    {
        public string Street { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Geo Geo { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public GeoDto Geo { get; set; }
    }
}
