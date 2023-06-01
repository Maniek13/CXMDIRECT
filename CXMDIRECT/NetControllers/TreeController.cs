using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Controllers;
using CXMDIRECT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CXMDIRECT.NetControllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TreeController : Controller
    {
        private readonly LogControllerAbstractClass logControllers;
        private readonly NodeControllerAbstractClass nodeController;
        private readonly string dbConnectionName = "CXMDIRECTConnection";
        public TreeController()
        {
            logControllers = new LogController(dbConnectionName);
            nodeController = new NodeController(dbConnectionName);
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
            catch (SecureException s)
            {
                return AddToLogs(s, parameters);
            }
            catch (Exception e)
            {
                return AddToLogs(e, parameters);
            }
        }

        [HttpGet("{id}")]
        public Response<dynamic> GetNode(int id)
        {
            List<(string name, string value)> parameters = new()
            {
                new("id", id.ToString())
            };

            try
            {
                Node node = nodeController.Get(id);

                return new Response<dynamic>()
                {
                    Type = "GetNode",
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

        [HttpPost()]
        public Response<dynamic> AddNode(int parrentId, string name, string description = "")
        {
            List<(string name, string value)> parameters = new()
            {
                new("parrentId", parrentId.ToString()),
                new("name", name),
                new("description", description)
            };

            try
            {
                Task<Node> task = Task.Run(async () => await nodeController.Add(parrentId, name, description));

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
                if (ae.InnerException != null)
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
            Task<ExceptionLog> task = Task.Run(async () => await logControllers.Add(exception, parameters));
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