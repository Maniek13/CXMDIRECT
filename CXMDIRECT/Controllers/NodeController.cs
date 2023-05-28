using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Models;

namespace CXMDIRECT.Controllers
{
    internal class NodeController : NodeControllerAbstractClass
    {
        internal override Node Add(int id, string name, string description) => throw new NotImplementedException();
        internal override Node Edit(int id, int parentId, string name, string description) => throw new NotImplementedException();
        internal override int Delete(int id) => throw new NotImplementedException();
    }
}
