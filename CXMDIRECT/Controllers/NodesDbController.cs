using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Data;
using CXMDIRECT.Models;
using Microsoft.EntityFrameworkCore;

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
        internal override async Task<NodeDbModel> Edit(int id, int parentId, string name, string description) => throw new NotImplementedException();
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
