using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Data;
using CXMDIRECT.Models;
using Microsoft.EntityFrameworkCore;

namespace CXMDIRECT.Controllers
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
