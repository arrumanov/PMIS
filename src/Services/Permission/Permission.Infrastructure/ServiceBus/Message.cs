namespace Permission.Infrastructure.ServiceBus
{
    public class Message
    {
        public string MessageType { get; set; }
        public string MessageData { get; private set; }

        public void SetData(object obj)
        {
            MessageData = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}