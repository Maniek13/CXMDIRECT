using CXMDIRECT.BaseClasses;
using CXMDIRECT.Models;

namespace CXMDIRECT.AbstractClasses
{
    internal abstract class LogDbControllerAbstractClass : DbControlerBaseClass
    {
        public LogDbControllerAbstractClass(string connectionString) : base(connectionString) { }
        internal virtual async Task<ExceptionLogDbModel> Add(ExceptionLogDbModel exception) => throw new NotImplementedException();
    }
}
