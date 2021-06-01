namespace AFLTips.Shared.Config
{
    public class SqlDbConfiguration
    {
        public SqlDbConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; }
    }
}
