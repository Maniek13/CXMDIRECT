using CXMDIRECT.Models;

namespace CXMDIRECT.AbstractClasses
{
    internal abstract class LogDbControllerAbstractClass
    {
        internal virtual async Task<ExceptionLogDbModel> Add(ExceptionLogDbModel exception) => throw new NotImplementedException();
    }
}
