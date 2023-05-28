using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Data;
using CXMDIRECT.Models;

namespace CXMDIRECT.Controllers
{
    internal class NodesDbController : NodeDbControllerAbstractClass
    {
        internal override async Task<NodeDbModel> Add(int parentId, string name, string description)
        {
            try
            {
                using var db = new CXMDIRECTDbContext();
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
        internal override NodeDbModel Edit(int id, int parentId, string name, string description)
        {
            try
            {
                using var db = new CXMDIRECTDbContext();

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
        internal override bool Delete(int id)
        {
            try
            {
                using var db = new CXMDIRECTDbContext();
                if (db.Nodes.Where(el => el.ParentId == id).FirstOrDefault() != null)
                    throw new SecureException("Object have a children, plese delete or edit it first");

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
