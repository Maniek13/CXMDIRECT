using CXMDIRECT.Controllers;
using CXMDIRECT.Data;
using CXMDIRECT.Models;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class NodeTests
    {
        public string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=G:\\Programowanie\\CXMDIRECT\\Tests\\TestDatabase.mdf;Integrated Security=True";
        [SetUp]
        public void Setup()
        {
         
        }

        [Test]
        public void TestDelete()
        {
            int id;

            using (CXMDIRECTDbContext db = new CXMDIRECTDbContext(connectionString))
            {

                NodeDbModel node = new NodeDbModel()
                {
                    ParentId = 0,
                    Description = "description",
                    Name = "name"
                };

                db.Add(node);
                db.SaveChanges();
                id = node.Id;
            }

            NodesDbController nodesDbController = new(connectionString);
            Assert.IsTrue(nodesDbController.Delete(id));
            Assert.Throws<SecureException>(() => nodesDbController.Delete(2));

            Assert.Throws<SecureException>(() => nodesDbController.Delete(-1));



            using (CXMDIRECTDbContext db = new CXMDIRECTDbContext(connectionString))
            {
                NodeDbModel parent = new NodeDbModel()
                {
                    ParentId = 0,
                    Description = "description",
                    Name = "name"
                };
                db.Add(parent);
                db.SaveChanges();

                NodeDbModel child = new NodeDbModel()
                {
                    ParentId = parent.Id,
                    Description = "description",
                    Name = "name"
                };

                db.Add(child);
                db.SaveChanges();
                id = parent.Id;

                Assert.Throws<SecureException>(() => nodesDbController.Delete(1));

                db.Remove(child);
                db.Remove(parent);
                db.SaveChanges();
            }
        }
    }
}