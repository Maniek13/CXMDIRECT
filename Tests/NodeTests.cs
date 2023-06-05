using CXMDIRECT.Data;
using CXMDIRECT.DbControllers;
using CXMDIRECT.DbModels;
using CXMDIRECT.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tests
{
    public class NodeTests
    {
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Mariusz\Programowanie\CXMDIRECT\CXMDIRECT\Tests\TestDb.mdf;Integrated Security = True";
        [SetUp]
        public void Setup()
        {
         
        }

        [Test]
        public void TestDelete()
        {
            int id;

            using (CXMDIRECTDbContext db = new(connectionString))
            {

                NodeDbModel node = new()
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
            Assert.That(nodesDbController.Delete(id), Is.True);
            Assert.Throws<SecureException>(() => nodesDbController.Delete(2));
            Assert.Throws<SecureException>(() => nodesDbController.Delete(-1));



            using (CXMDIRECTDbContext db = new(connectionString))
            {
                NodeDbModel parent = new()
                {
                    ParentId = 0,
                    Description = "description",
                    Name = "name"
                };
                db.Add(parent);
                db.SaveChanges();

                NodeDbModel child = new()
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
        [Test]
        public void TestGet()
        {
            int id;
            NodeDbModel node = new();
            using (CXMDIRECTDbContext db = new(connectionString))
            {

                node = new()
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
            Assert.Multiple(() =>
            {
                Assert.That(nodesDbController.Get(id).Id, Is.EqualTo(node.Id));
                Assert.That(nodesDbController.Get(id).ParentId, Is.EqualTo(node.ParentId));
            });
            Assert.Throws<SecureException>(() => nodesDbController.Get(id+1));

            nodesDbController.Delete(id);
        }

        [Test]
        public async Task TestAdd()
        {
            NodesDbController nodesDbController = new(connectionString);

            NodeDbModel model = await nodesDbController.Add(0,"nowe", "xxx");

            Assert.That(model.Name == "nowe" && model.Description == "xxx");

            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Add(-1, "nowe", "xxx"));
            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Add(model.Id+100, "nowe", "xxx"));
            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Add(model.Id + 100, "nowe", "xxx"));
            nodesDbController.Delete(model.Id);

            model = await nodesDbController.Add(0, "nowe", null);
            Assert.That(model.Name == "nowe" && model.Description == null);
            nodesDbController.Delete(model.Id);

            model = await nodesDbController.Add(null, "nowe", null);
            Assert.That(model.Name == "nowe" && model.Description == null && model.ParentId == 0);
            nodesDbController.Delete(model.Id);
        }

        [Test]
        public async Task TestUpdate()
        {
            NodesDbController nodesDbController = new(connectionString);

            NodeDbModel model = await nodesDbController.Add(0, "nowe", "xxx");

            NodeDbModel edited = nodesDbController.Edit(model.Id, 0, "edytowane", "nowy opis");

            Assert.That(edited.Name == "edytowane" && edited.Description == "nowy opis");

            edited = nodesDbController.Edit(model.Id, 0, "edytowane", null);
            Assert.That(edited.Name == "edytowane" && edited.Description == null);

            Assert.Throws<SecureException>(() => nodesDbController.Edit(-1, 0, "nowe", "xxx"));
            Assert.Throws<SecureException>(() => nodesDbController.Edit(0, 0, "nowe", "xxx"));
            Assert.Throws<SecureException>(() => nodesDbController.Edit(edited.Id, -1, "nowe", "xxx"));

            Assert.Throws<SecureException>(() => nodesDbController.Edit(12, 1, "nowe", "xxx"));
            Assert.Throws<SecureException>(() => nodesDbController.Edit(edited.Id, 5, "nowe", "xxx"));

            nodesDbController.Delete(model.Id);

        }
    }
}