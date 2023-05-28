using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Models;

namespace CXMDIRECT.Controllers
{
    internal class NodeController : NodeControllerAbstractClass
    {
        internal override async Task<Node> Add(int parentId, string name, string description) 
        {

            try
            {
                if (parentId < 0)
                {
                    throw new SecureException("The parent id must be greater or equal then 0");
                }

                NodesDbController nodesDbController = new();
                NodeDbModel node = await nodesDbController.Add(parentId, name, description);

                return ConvertToNode(node);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal override Node Edit(int id, int parentId, string name, string description) => throw new NotImplementedException();
        internal override int Delete(int id) => throw new NotImplementedException();

        private Node ConvertToNode(NodeDbModel node)
        {
            return new Node()
            {
                Id = node.Id,
                ParentId = node.ParentId,
                Name = node.Name,
                Description = node.Description
            };
        }
    }
}
