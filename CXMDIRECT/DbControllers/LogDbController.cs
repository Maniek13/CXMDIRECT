using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Data;

namespace CXMDIRECT.DbControllers
{
    internal class LogDbController : LogDbControllerAbstractClass
    {
        internal LogDbController(string connectionString) : base(connectionString) { }
        internal override async Task<ExceptionLogDbModel> Add(ExceptionLogDbModel exception)
        {
            try
            {
                using var db = new CXMDIRECTDbContext(_connectionString);

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
