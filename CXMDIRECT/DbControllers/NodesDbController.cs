using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Data;
using CXMDIRECT.DbModels;
using CXMDIRECT.Models;

namespace CXMDIRECT.DbControllers
{
    public class NodesDbController : NodeDbControllerAbstractClass
    {
        public NodesDbController(string connectionString) : base(connectionString) { }

        public override NodeDbModel Get(int id)
        {
            try
            {
                if (id < 0)
                    throw new SecureException("The node id must be greater 0");

                using var db = new CXMDIRECTDbContext(_connectionString);

                NodeDbModel node = db.Nodes.Where(el => el.Id == id).FirstOrDefault();

                if (node == null)
                    throw new SecureException($"No node with id {id}");

                return node;
            }
            catch (SecureException s)
            {
                throw new SecureException(s.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public override async Task<NodeDbModel> Add(int parentId, string name, string description)
        {
            try
            {
                if (parentId < 0)
                    throw new SecureException("The parent id must be greater or equal 0");

                using var db = new CXMDIRECTDbContext(_connectionString);

                if (parentId != 0 && db.Nodes.Where(el => el.Id == parentId).FirstOrDefault() == null)
                    throw new SecureException("Parent doesn't exist");

                NodeDbModel node = new()
                {
                    ParentId = parentId,
                    Name = name,
                    Description = description
                };

                await db.AddAsync(node);
                await db.SaveChangesAsync();
                return node;
            }
            catch(SecureException s)
            {
                throw new SecureException(s.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public override NodeDbModel Edit(int id, int parentId, string name, string description)
        {
            try
            {
                if (id <= 0)
                    throw new SecureException("The node id must be greater 0");
                if (parentId < 0)
                    throw new SecureException("The paren id must be greater or equal 0");

                using var db = new CXMDIRECTDbContext(_connectionString);

                NodeDbModel? node = db.Nodes.Where(el => el.Id == id).FirstOrDefault();

                if (parentId != 0 && node == null)
                    throw new SecureException("Node doesn't exist");
                if (parentId != 0 && db.Nodes.Where(el => el.Id == parentId).FirstOrDefault() == null)
                    throw new SecureException("Parent doesn't exist");

                node.ParentId = parentId;
                node.Name = name;
                node.Description = description;

                db.Nodes.Update(node);
                db.SaveChanges();

                return node;
            }
            catch (SecureException s)
            {
                throw new SecureException(s.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        public override bool Delete(int id)
        {
            try
            {
                if (id < 0)
                    throw new SecureException("The node id must be greater 0");

                using var db = new CXMDIRECTDbContext(_connectionString);

                if (db.Nodes.Where(el => el.Id == id).FirstOrDefault() == null)
                    throw new SecureException("No object in database");

                if (db.Nodes.Where(el => el.ParentId == id).FirstOrDefault() != null)
                    throw new SecureException("Object have a children, plese delete or edit it first");
                
                db.ChangeTracker.Clear();
                db.Nodes.Remove(new NodeDbModel()
                {
                    Id = id,
                });

                db.SaveChanges();
                return true;
            }
            catch (SecureException s)
            {
                throw new SecureException(s.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
