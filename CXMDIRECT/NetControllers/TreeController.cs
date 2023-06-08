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
        private readonly string connectionString = "CXMDIRECTConnection";
        public TreeController()
        {
            logControllers = new LogController(connectionString);
            nodeController = new NodeController(connectionString);
        }

        #region http functions
        [HttpGet("{id}")]
        public async Task<Response<dynamic>> GetNode(int id)
        {
            List<(string name, string? value)> parameters = new()
            {
                new("id", id.ToString())
            };

            try
            {
                Node node = await nodeController.Get(id);

                return new Response<dynamic>()
                {
                    Type = "GetNode",
                    Id = 1,
                    Data = node
                };
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
        public async Task<Response<dynamic>> AddNode(int? parrentId, string name, string? description)
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
        public async Task<Response<dynamic>> EditNode(int id, int parrentId, string name, string? description)
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

                return new Response<dynamic>()
                {
                    Type = "EditNode",
                    Id = 1,
                    Data = node
                };
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
        public async Task<Response<dynamic>> DeleteNode(int id)
        {
            List<(string name, string? value)> parameters = new()
            {
                new("id", id.ToString())
            };

            try
            {
                bool res = await nodeController.Delete(id);
                if (!res)
                    throw new SecureException("Not was not delete");

                return new Response<dynamic>()
                {
                    Type = "DeleteNode",
                    Id = 1,
                    Data = true
                };
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
        private async Task<Response<dynamic>> AddToLogs(Exception exception, List<(string name, string? value)> parameters)
        {
            ExceptionLog log = await logControllers.Add(exception, parameters);

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
        #endregion
    }
}