using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Models;
using Microsoft.EntityFrameworkCore;

namespace CXMDIRECT.Controllers
{
    internal class NodesDbController : NodeDbControllerAbstractClass
    {
        internal override NodeDbModel Add(int id, string name, string description) => throw new NotImplementedException();
        internal override NodeDbModel Edit(int id, int parentId, string name, string description) => throw new NotImplementedException();
        internal override int Delete(int id) => throw new NotImplementedException();
    }
}
