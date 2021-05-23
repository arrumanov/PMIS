namespace ProjectPortfolio.Infrastructure.ServiceBus
{
    public class BusConfiguration
    {
        public string VirtualHost { get; set; }
        public string Url { get; set; }
        public string Queue { get; set; }
        public BusTransport Transport { get; set; }
        public BusCrendentials Credentials { get; set; }
        public string HostName { get; set; }
    }
}