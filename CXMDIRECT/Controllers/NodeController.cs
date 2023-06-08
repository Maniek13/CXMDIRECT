using CXMDIRECT.AbstractClasses;
using CXMDIRECT.DbControllers;
using CXMDIRECT.DbModels;
using CXMDIRECT.Models;

namespace CXMDIRECT.Controllers
{
    internal class NodeController : NodeControllerAbstractClass
    {
        private readonly NodesDbController _dbController;

        internal NodeController(string dbConnection)
        {
            _dbController = new NodesDbController(dbConnection);
        }
        internal override async Task<Node> Get(int id)
        {
            try
            {
                return ConvertToNode(await _dbController.Get(id));

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
        internal override async Task<Node> Add(int? parentId, string name, string? description) 
        {
            try
            {
                NodeDbModel node = await _dbController.Add(parentId, name, description);

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
        internal override async Task <Node> Edit(int id, int parentId, string name, string? description)
        {
            try
            {
                return ConvertToNode(await _dbController.Edit(id, parentId, name, description));
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
        internal override async Task<bool> Delete(int id)
        {
            try
            {
                if (await _dbController.Delete(id) == false)
                    throw new SecureException("Problem with delete, check is object exist in database");
                return true;

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
        #region private functions
        private static Node ConvertToNode(NodeDbModel node)
        {
            return new Node()
            {
                Id = node.Id,
                ParentId = node.ParentId,
                Name = node.Name,
                Description = node.Description
            };
        }
        #endregion
    }
}
