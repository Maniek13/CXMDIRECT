using CXMDIRECT.BaseClasses;
using CXMDIRECT.Models;

namespace CXMDIRECT.AbstractClasses
{
    public abstract class LogDbControllerAbstractClass : DbControlerBaseClass
    {
        public LogDbControllerAbstractClass(string connectionString) : base(connectionString) { }
        public virtual async Task<ExceptionLogDbModel> Add(ExceptionLogDbModel exception) => throw new NotImplementedException();
    }
}
