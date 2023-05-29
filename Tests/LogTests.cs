using CXMDIRECT.Controllers;
using CXMDIRECT.Data;
using CXMDIRECT.Models;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class LogTests
    {
        public string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=G:\\Programowanie\\CXMDIRECT\\Tests\\TestDatabase.mdf;Integrated Security=True";
        [SetUp]
        public void Setup()
        {
         
        }

        [Test]
        public async Task TestAdd()
        {
            LogDbController logDbController = new(connectionString);

            ExceptionLogDbModel log =  await logDbController.Add(new ExceptionLogDbModel());

            Assert.IsNotNull(log);


            using (CXMDIRECTDbContext db = new CXMDIRECTDbContext(connectionString))
            {
                ExceptionLogDbModel? elToCompare = db.ExceptionsLogs.Where(el => el.Id == log.Id).FirstOrDefault();
                Assert.IsTrue(elToCompare != null);
            }
        }
    }
}