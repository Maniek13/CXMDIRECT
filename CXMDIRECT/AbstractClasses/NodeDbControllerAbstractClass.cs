using CXMDIRECT.Models;

namespace CXMDIRECT.AbstractClasses
{
    internal abstract class NodeDbControllerAbstractClass
    {
        internal virtual async Task<NodeDbModel> Add(int parentId, string name, string description) => throw new NotImplementedException();
        internal abstract NodeDbModel Edit(int id, int parentId, string name, string description);
        internal abstract int Delete(int id);
    }
}
