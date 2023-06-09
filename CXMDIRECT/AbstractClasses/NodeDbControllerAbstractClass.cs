using CXMDIRECT.BaseClasses;

namespace CXMDIRECT.AbstractClasses
{
    public abstract class NodeDbControllerAbstractClass : DbControlerBaseClass
    {
        public NodeDbControllerAbstractClass(string connectionString) : base(connectionString) { }
        public virtual async Task<NodeDbModel> Get(int id) => throw new NotImplementedException();
        public virtual async Task<NodeDbModel> Add(int? parentId, string name, string? description) => throw new NotImplementedException();
        public virtual async Task<NodeDbModel> Edit(int id, int parentId, string name, string? description) => throw new NotImplementedException();
        public virtual async Task<bool> Delete(int id) => throw new NotImplementedException();
    }
}
