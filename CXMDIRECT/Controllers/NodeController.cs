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
                    throw new SecureException("The parent id must be greater or equal 0");
                }

                NodesDbController nodesDbController = new();
                NodeDbModel node = await nodesDbController.Add(parentId, name, description);

                return ConvertToNode(node);

            }
            catch (SecureException s)
            {
                throw new SecureException(s.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal override Node Edit(int id, int parentId, string name, string description)
        {
            try
            {
                if (id <= 0)
                {
                    throw new SecureException("The node id must be greater 0");
                }
                if (parentId < 0)
                {
                    throw new SecureException("The paren id must be greater or equal 0");
                }

                NodesDbController nodesDbController = new();

                return ConvertToNode(nodesDbController.Edit(id, parentId, name, description));
            }
            catch (SecureException s)
            {
                throw new SecureException(s.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal override void Delete(int id)
        {
            try
            {
                if (id < 0)
                {
                    throw new SecureException("The node id must be greater 0");
                }

                NodesDbController nodeDbController = new();

                if (nodeDbController.Delete(id) == false)
                    throw new SecureException("Problem with delete, check is object exist in database");

            }
            catch (SecureException s)
            {
                throw new SecureException(s.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

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
