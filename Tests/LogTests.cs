using CXMDIRECT.Data;
using CXMDIRECT.DbControllers;
using CXMDIRECT.DbModels;

namespace Tests
{
    public class LogTests 
    {
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Programowanie\CXMDIRECT\Tests\TestDatabase.mdf;Integrated Security=True";
        [SetUp]
        public void Setup()
        {
         
        }

        [Test]
        public async Task TestAdd()
        {
            LogDbController logDbController = new(connectionString);
            ExceptionLogDbModel log =  await logDbController.Add(new ExceptionLogDbModel());

            Assert.That(log, Is.Not.Null);

            using CXMDIRECTDbContext db = new(connectionString);
            ExceptionLogDbModel? elToCompare = db.ExceptionsLogs.Where(el => el.Id == log.Id).FirstOrDefault();
            Assert.That(elToCompare, Is.Not.EqualTo(null));
        }
    }
}