namespace CXMDIRECT.BaseClasses
{
    public class DbControlerBaseClass
    {
        internal readonly string _connectionString;

        public DbControlerBaseClass(string connectionString)
        {
            _connectionString = connectionString;   
        }
    }
}
