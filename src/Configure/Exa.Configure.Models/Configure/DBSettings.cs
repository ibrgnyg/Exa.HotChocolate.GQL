namespace Exa.Configure.Models.Configure
{
    public class DBSettings : IDBSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
