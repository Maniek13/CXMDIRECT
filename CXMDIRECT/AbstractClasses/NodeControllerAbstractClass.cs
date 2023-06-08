using CXMDIRECT.Models;

namespace CXMDIRECT.AbstractClasses
{
    internal abstract class NodeControllerAbstractClass
    {
        internal virtual async Task<Node> Get(int id) => throw new NotImplementedException();
        internal virtual async Task<Node> Add(int? parentId, string name, string? description) => throw new NotImplementedException();
        internal virtual async Task<Node> Edit(int id, int parentId, string name, string? description) => throw new NotImplementedException();
        internal virtual async Task<bool> Delete(int id) => throw new NotImplementedException();
    }
}
