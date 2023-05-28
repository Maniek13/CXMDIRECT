using CXMDIRECT.Models;

namespace CXMDIRECT.AbstractClasses
{
    internal abstract class NodeDbControllerAbstractClass
    {
        internal abstract NodeDbModel Add(int parentId, string name, string description);
        internal abstract NodeDbModel Edit(int id, int parentId, string name, string description);
        internal abstract int Delete(int id);
    }
}
