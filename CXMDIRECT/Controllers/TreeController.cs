using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Models;
using Microsoft.AspNetCore.Mvc;

namespace CXMDIRECT.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TreeController : ControllerBase
    {
        private readonly LogControllerAbstractClass logControllers;
        private readonly NodeControllerAbstractClass nodeController;
        private readonly string dbConnectionName = "CXMDIRECTConnection";
        public TreeController()
        { 
            logControllers = new LogController(dbConnectionName);
            nodeController = new NodeController(dbConnectionName);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Empty;
        }  
        
        [HttpDelete("{id}")]
        public Response<dynamic> DeleteNode(int id)
        {
            List<(string name, string value)> parameters = new()
            {
                new("id", id.ToString())
            };

            try
            {
                nodeController.Delete(id);

                return new Response<dynamic>()
                {
                    Type = "DeleteNode",
                    Id = 1,
                    Data = true
                };
            }
            catch(SecureException s)
            {
                return AddToLogs(s, parameters);
            }
            catch(Exception e)
            {
                return AddToLogs(e, parameters);
            }
        }

        [HttpPost()]
        public Response<dynamic> AddNode(int parrentId, string name, string description = "")
        {
            List<(string name, string value)> parameters = new();

            parameters.Add(new("parrentId", parrentId.ToString()));
            parameters.Add(new("name", name));
            parameters.Add(new("description", description));

            try
            {
                Task<Node> task = Task.Run<Node>(async () => await nodeController.Add(parrentId, name, description));
                
                Node node = task.Result;

                return new Response<dynamic>()
                {
                    Type = "AddNode",
                    Id = 1,
                    Data = node
                };
            }
            catch (AggregateException ae)
            {
                if(ae.InnerException != null)
                    return AddToLogs(ae.InnerException, parameters);
                else
                    return AddToLogs(ae, parameters);
            }
            catch (SecureException s)
            {
                return AddToLogs(s, parameters);
            }
            catch (Exception e)
            {
                return AddToLogs(e, parameters);
            }
        }

        [HttpPost()]
        public Response<dynamic> EditNode(int id, int parrentId, string name, string description = "")
        {
            List<(string name, string value)> parameters = new()
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

                Node node = nodeController.Edit(id, parrentId, name, description);

                return new Response<dynamic>()
                {
                    Type = "EditNode",
                    Id = 1,
                    Data = node
                };

            }
            catch (SecureException s)
            {
                return AddToLogs(s, parameters);
            }
            catch (Exception e)
            {

                return AddToLogs(e, parameters);
            }
        }


        private Response<dynamic> AddToLogs(Exception exception, List<(string name, string value)> parameters)
        {
            Task<ExceptionLog> task = Task.Run<ExceptionLog>(async () => await logControllers.Add(exception, parameters));
            ExceptionLog log = task.Result;

            Response.StatusCode = 500;

            return new Response<dynamic>()
            {
                Type = log.ExtensionType,
                Id = log.Id,
                Data = new ExceptionData()
                {
                    Message = log.Message
                }
            };
        }
    }
}