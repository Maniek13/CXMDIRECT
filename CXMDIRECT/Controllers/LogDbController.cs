using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Data;
using CXMDIRECT.Models;

namespace CXMDIRECT.Controllers
{
    internal class LogDbController : LogDbControllerAbstractClass
    {
        internal override async Task<ExceptionLogDbModel> Add(ExceptionLogDbModel exception)
        {
            try
            {
                using var db = new CXMDIRECTDbContext();
                await db.ExceptionsLogs.AddAsync(exception);
                await db.SaveChangesAsync();
                return exception;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
    }
}
