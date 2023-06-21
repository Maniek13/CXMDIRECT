using CXMDIRECT.BaseClasses;

namespace CXMDIRECT.AbstractClasses
{
    internal abstract class LogDbControllerAbstractClass : DbControlerBaseClass
    {
        internal LogDbControllerAbstractClass(string connectionString) : base(connectionString) { }
        internal virtual async Task<ExceptionLogDbModel> Add(ExceptionLogDbModel exception) => throw new NotImplementedException();
    }
}
