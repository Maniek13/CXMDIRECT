using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CXMDIRECT.NetControllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TreeController : Controller
    {
        private readonly LogControllerAbstractClass logControllers;
        private readonly NodeControllerAbstractClass nodeController;
        private readonly string connectionString = "CXMDIRECTConnection";
        public TreeController()
        {
            logControllers = new LogController(connectionString);
            nodeController = new NodeController(connectionString);
        }

        #region http functions
        [HttpGet("{id}")]
        public async Task<ObjectResult> GetNode(int id)
        {
            List<(string name, string? value)> parameters = new()
            {
                new("id", id.ToString())
            };

            try
            {
                Node node = await nodeController.Get(id);

                return StatusCode(200, new Response<dynamic>("GetNode", 1, node));
            }
            catch (SecureException s)
            {
                return await AddToLogs(s, parameters);
            }
            catch (Exception e)
            {
                return await AddToLogs(e, parameters);
            }
        }

        [HttpPost()]
        public async Task<ObjectResult> AddNode(int? parrentId, string name, string? description)
        {
            List<(string name, string? value)> parameters = new()
            {
                new("parrentId", parrentId.ToString()),
                new("name", name),
                new("description", description)
            };

            try
            {
                Node node = await nodeController.Add(parrentId, name, description);

                return StatusCode(200, new Response<dynamic>("AddNode", 1, node));
            }
            catch (AggregateException ae)
            {
                if (ae.InnerException != null)
                    return await AddToLogs(ae.InnerException, parameters);
                else
                    return await AddToLogs(ae, parameters);
            }
            catch (SecureException s)
            {
                return await AddToLogs(s, parameters);
            }
            catch (Exception e)
            {
                return await AddToLogs(e, parameters);
            }
        }

        [HttpPost()]
        public async Task<ObjectResult> EditNode(int id, int parrentId, string name, string? description)
        {

            List<(string name, string? value)> parameters = new()
            {
                new("id", id.ToString()),
                new("parrentId", parrentId.ToString()),
                new("name", name),
                new("description", description)
            };

            try
            {
                if (parrentId < 0)
                {
                    throw new SecureException("The parent id must be greater or equal 0");
                }

                Node node = await nodeController.Edit(id, parrentId, name, description);

                return StatusCode(200, new Response<dynamic>("EditNode", 1, node));
            }
            catch (SecureException s)
            {
                return await AddToLogs(s, parameters);
            }
            catch (Exception e)
            {

                return await AddToLogs(e, parameters);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ObjectResult> DeleteNode(int id)
        {
            List<(string name, string? value)> parameters = new()
            {
                new("id", id.ToString())
            };

            try
            {
                bool res = await nodeController.Delete(id);
                if (!res)
                    throw new SecureException("Node was not deleted");

                return StatusCode(200, new Response<dynamic>("DeleteNode", 1, true));
            }
            catch (SecureException s)
            {
                return await AddToLogs(s, parameters);
            }
            catch (Exception e)
            {
                return await AddToLogs(e, parameters);
            }
        }
        #endregion

        #region private functions
        private async Task<ObjectResult> AddToLogs(Exception exception, List<(string name, string? value)> parameters)
        {
            ExceptionLog log = await logControllers.Add(exception, parameters);

            return StatusCode(500, new Response<dynamic>(log.ExtensionType, log.Id, new ExceptionData(log.Message)));
        }
        #endregion
    }
}