using CXMDIRECT.BaseClasses;
using CXMDIRECT.Models;
using Microsoft.AspNetCore.Mvc;

namespace CXMDIRECT.AbstractClasses
{
    public abstract class NodeDbControllerAbstractClass : DbControlerBaseClass
    {
        public NodeDbControllerAbstractClass(string connectionString) : base(connectionString) { }
        public virtual async Task<NodeDbModel> Add(int parentId, string name, string description) => throw new NotImplementedException();
        public abstract NodeDbModel Edit(int id, int parentId, string name, string description);
        public abstract bool Delete(int id);
    }
}
