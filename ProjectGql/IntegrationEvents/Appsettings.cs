namespace PMIS.ProjectGql.IntegrationEvents
{
    public class Appsettings
    {
        public QueueSettings QueueSettings { get; set; }

        public Appsettings()
        {

        }

        public Appsettings(QueueSettings queueSettings)
        {
            QueueSettings = queueSettings;
        }
    }
}