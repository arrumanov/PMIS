namespace Dogovor.Infrastructure.ServiceBus
{
    public class BusCrendentials
    {
        // RabbitMQ
        public string Username { get; set; }
        public string Password { get; set; }

        // Azure
        public string KeyName { get; set; }
        public string SharedAccessKey { get; set; }
        public int TokenTimeToLive { get; set; }
    }
}