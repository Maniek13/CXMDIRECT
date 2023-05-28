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
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal override NodeDbModel Edit(int id, int parentId, string name, string description) => throw new NotImplementedException();
        internal override int Delete(int id) => throw new NotImplementedException();
    }
}
