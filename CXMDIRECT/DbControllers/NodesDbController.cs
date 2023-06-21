using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Data;
using CXMDIRECT.DbModels;
using CXMDIRECT.Models;

namespace CXMDIRECT.DbControllers
{
    internal class NodesDbController : NodeDbControllerAbstractClass
    {
        internal NodesDbController(string connectionString) : base(connectionString) { }

        internal override async Task<NodeDbModel> Get(int id)
        {
            try
            {
                if (id < 0)
                    throw new SecureException("The node id must be greater 0");

                using var db = new CXMDIRECTDbContext(_connectionString);

                NodeDbModel? node = db.Nodes.Where(el => el.Id == id).FirstOrDefault();

                return node ?? throw new SecureException($"No node with id {id}");
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
        internal override async Task<NodeDbModel> Add(int? parentId, string name, string? description)
        {
            try
            {
                parentId ??= 0;

                if (parentId < 0)
                    throw new SecureException("The parent id must be greater or equal 0");

                if (string.IsNullOrEmpty(name))
                    throw new SecureException("The name can't be empty");

                using var db = new CXMDIRECTDbContext(_connectionString);

                if (parentId != 0 && db.Nodes.Where(el => el.Id == parentId).FirstOrDefault() == null)
                    throw new SecureException("Parent doesn't exist");


                NodeDbModel node = new()
                {
                    ParentId = (int)parentId,
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
        internal override async Task <NodeDbModel> Edit(int id, int parentId, string name, string? description)
        {
            try
            {
                if (id <= 0)
                    throw new SecureException("The node id must be greater 0");
                if (parentId < 0)
                    throw new SecureException("The paren id must be greater or equal 0");

                using var db = new CXMDIRECTDbContext(_connectionString);

                NodeDbModel node = db.Nodes.Where(el => el.Id == id).FirstOrDefault() ?? throw new SecureException("Node doesn't exist");

                if (parentId != 0 && db.Nodes.Where(el => el.Id == parentId).FirstOrDefault() == null)
                    throw new SecureException("Parent doesn't exist");

       
                node.ParentId = parentId;
                node.Name = name;
                node.Description = description;

                await db.SaveChangesAsync();

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
        internal override async Task<bool> Delete(int id)
        {
            try
            {
                
                if (id < 0)
                    throw new SecureException("The node id must be greater 0");

                using var db = new CXMDIRECTDbContext(_connectionString);

                NodeDbModel? node = db.Nodes.Where(el => el.Id == id).FirstOrDefault() ?? throw new SecureException("No object in database");
                
                if (db.Nodes.Where(el => el.ParentId == id).FirstOrDefault() != null)
                    throw new SecureException("Object have a children, please delete or edit it first");

                db.Nodes.Remove(node);
                await db.SaveChangesAsync();
               
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
