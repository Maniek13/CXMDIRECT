using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Data;
using CXMDIRECT.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CXMDIRECT.DbControllers
{
    public class LogDbController : LogDbControllerAbstractClass
    {
        public LogDbController(string connectionString) : base(connectionString) { }
        public override async Task<ExceptionLogDbModel> Add(ExceptionLogDbModel exception)
        {
            try
            {
                using var db = new CXMDIRECTDbContext(_connectionString);

                DbContextOptionsBuilder dbOptionsBuilder = new();

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
