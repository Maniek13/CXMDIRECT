using CXMDIRECT.Data;
using CXMDIRECT.DbControllers;
using CXMDIRECT.DbModels;
using CXMDIRECT.Models;
using NUnit.Framework.Internal;

namespace Tests
{
    public class NodeTests
    {
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Programowanie\CXMDIRECT\Tests\TestDatabase.mdf;Integrated Security=True";
        [SetUp]
        public void Setup()
        {
         
        }

        [Test]
        public async Task TestDelete()
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
            bool res = await nodesDbController.Delete(id);
            Assert.That(res, Is.True);
            using (CXMDIRECTDbContext db = new(connectionString))
            {
                var test = db.Nodes.Where(el => el.Id == id).FirstOrDefault();
                if (test is not null)
                    Assert.Fail();
            }

            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Delete(2));
            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Delete(-1));

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

                Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Delete(1));

                db.Remove(child);
                db.Remove(parent);
                db.SaveChanges();
            }
        }
        [Test]
        public async Task TestGet()
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
                await db.SaveChangesAsync();
                id = node.Id;
            }
            NodesDbController nodesDbController = new(connectionString);
            NodeDbModel getNode = await nodesDbController.Get(id);
            Assert.Multiple(() =>
            {
                Assert.That(getNode.Id, Is.EqualTo(node.Id));
                Assert.That(getNode.ParentId, Is.EqualTo(node.ParentId));
            });
            Assert.ThrowsAsync<SecureException>(async () =>await  nodesDbController.Get(id+1));

            await nodesDbController.Delete(id);
        }

        [Test]
        public async Task TestAdd()
        {
            NodesDbController nodesDbController = new(connectionString);

            NodeDbModel model = await nodesDbController.Add(0,"nowe", "xxx");
            using (CXMDIRECTDbContext db = new(connectionString))
            {
                var test = db.Nodes.Where(el => el.Id == model.Id).FirstOrDefault();
                Assert.That(model.Name == test.Name && model.Description == test.Description);
            }
            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Add(-1, "nowe", "xxx"));
            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Add(model.Id+100, "nowe", "xxx"));
            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Add(model.Id + 100, "nowe", "xxx"));
            await nodesDbController.Delete(model.Id);

            model = await nodesDbController.Add(0, "nowe", null);
            using (CXMDIRECTDbContext db = new(connectionString))
            {
                var test = db.Nodes.Where(el => el.Id == model.Id).FirstOrDefault();
                Assert.That(model.Name == test.Name && model.Description == test.Description);
            }
            await nodesDbController.Delete(model.Id);

            model = await nodesDbController.Add(null, "nowe", null);
            using (CXMDIRECTDbContext db = new(connectionString))
            {
                var test = db.Nodes.Where(el => el.Id == model.Id).FirstOrDefault();
                Assert.That(model.Name == test.Name && model.Description == test.Description && model.ParentId == test.ParentId);
            }
            await nodesDbController.Delete(model.Id);
        }

        [Test]
        public async Task TestUpdate()
        {
            NodesDbController nodesDbController = new(connectionString);
            NodeDbModel model = await nodesDbController.Add(0, "nowe", "xxx");

            NodeDbModel edited = await nodesDbController.Edit(model.Id, 0, "edytowane", "nowy opis");
            using (CXMDIRECTDbContext db = new(connectionString))
            {
                var test = db.Nodes.Where(el => el.Id == edited.Id).FirstOrDefault();
                Assert.That(edited.Name == test.Name && edited.Description == test.Description);
            }

            edited = await nodesDbController.Edit(model.Id, 0, "edytowane", null);
            using (CXMDIRECTDbContext db = new(connectionString))
            {
                var test = db.Nodes.Where(el => el.Id == edited.Id).FirstOrDefault();
                Assert.That(edited.Name == test.Name && edited.Description == test.Description);
            }

            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Edit(-1, 0, "nowe", "xxx"));
            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Edit(0, 0, "nowe", "xxx"));
            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Edit(edited.Id, -1, "nowe", "xxx"));
            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Edit(12, 1, "nowe", "xxx"));
            Assert.ThrowsAsync<SecureException>(async () => await nodesDbController.Edit(edited.Id, 5, "nowe", "xxx"));

            await nodesDbController.Delete(model.Id);

        }
    }
}