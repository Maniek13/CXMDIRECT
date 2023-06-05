using CXMDIRECT.Models;

namespace CXMDIRECT.AbstractClasses
{
    internal abstract class LogControllerAbstractClass
    {
        internal virtual async Task<ExceptionLog> Add(Exception exception, List<(string name, string? value)> parameters) => throw new NotImplementedException();
    }
}
