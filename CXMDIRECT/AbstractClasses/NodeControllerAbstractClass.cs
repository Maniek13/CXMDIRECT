using CXMDIRECT.Models;

namespace CXMDIRECT.AbstractClasses
{
    internal abstract class NodeControllerAbstractClass
    {
        internal abstract Node Get(int id);
        internal virtual async Task<Node> Add(int parentId, string name, string description) => throw new NotImplementedException();
        internal abstract Node Edit(int id, int parentId, string name, string description);
        internal abstract void Delete(int id);
    }
}
