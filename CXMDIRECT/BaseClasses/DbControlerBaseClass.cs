namespace CXMDIRECT.BaseClasses
{
    internal class DbControlerBaseClass
    {
        internal readonly string _connectionString;

        protected DbControlerBaseClass(string connectionString)
        {
            _connectionString = connectionString;   
        }
    }
}
