namespace Dogovor.Infrastructure.Database
{
    public class DatabaseConfiguration
    {
        public DatabaseProvider WriteDatabaseProvider { get; set; }
        public string WriteDatabase { get; set; }
        public DatabaseProvider ReadDatabaseProvider { get; set; }
        public string ReadDatabase { get; set; }
    }
}