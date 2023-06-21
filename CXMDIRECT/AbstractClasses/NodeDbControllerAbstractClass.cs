using CXMDIRECT.BaseClasses;

namespace CXMDIRECT.AbstractClasses
{
    internal abstract class NodeDbControllerAbstractClass : DbControlerBaseClass
    {
        internal NodeDbControllerAbstractClass(string connectionString) : base(connectionString) { }
        internal virtual async Task<NodeDbModel> Get(int id) => throw new NotImplementedException();
        internal virtual async Task<NodeDbModel> Add(int? parentId, string name, string? description) => throw new NotImplementedException();
        internal virtual async Task<NodeDbModel> Edit(int id, int parentId, string name, string? description) => throw new NotImplementedException();
        internal virtual async Task<bool> Delete(int id) => throw new NotImplementedException();
    }
}
