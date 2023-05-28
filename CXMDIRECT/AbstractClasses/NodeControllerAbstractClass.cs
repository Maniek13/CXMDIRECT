using CXMDIRECT.Models;

namespace CXMDIRECT.AbstractClasses
{
    internal abstract class NodeControllerAbstractClass
    {
        internal abstract Node Add(int parentId, string name, string description);
        internal abstract Node Edit(int id, int parentId, string name, string description);
        internal abstract int Delete(int id);
    }
}
