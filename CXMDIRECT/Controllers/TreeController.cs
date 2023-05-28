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
        public TreeController()
        { 
            logControllers = new LogControllers();
            nodeController = new NodeController();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Empty;
        }  
        
        [HttpDelete("{id}")]
        public Response<dynamic> DeleteNode(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new SecureException("The node id must be greater or equal then 0");
                }

                throw new NotImplementedException();
            }
            catch(SecureException s)
            {
                return AddToLogs(s);
            }
            catch(System.Exception e)
            {
               return AddToLogs(e);
            }
        }

        [HttpPost()]
        public Response<dynamic> AddNode(int parrentId, string name, string description = "")
        {
            try
            {
                if (parrentId < 0)
                {
                    throw new SecureException("The parent id must be greater or equal then 0");
                }

                Node node = nodeController.Add(parrentId, name, description);


                throw new NotImplementedException();

            }
            catch (SecureException s)
            {
                return AddToLogs(s);
            }
            catch (System.Exception e)
            {
                return AddToLogs(e);
            }
        }

        [HttpPost()]
        public Response<dynamic> EditNode(int id, int parrentId, string name, string description = "")
        {
            try
            {
                if (parrentId < 0)
                {
                    throw new SecureException("The parent id must be greater or equal then 0");
                }

                Node node = nodeController.Edit(id, parrentId, name, description);


                throw new NotImplementedException();

            }
            catch (SecureException s)
            {
                return AddToLogs(s);
            }
            catch (System.Exception e)
            {
                return AddToLogs(e);
            }
        }


        private Response<dynamic> AddToLogs(System.Exception e)
        {
            Log log = logControllers.Add(e);

            Response.StatusCode = 500;

            return new Response<dynamic>()
            {
                Type = e.GetType().Name,
                Id = log.Id,
                Data = new Models.Exception
                {
                    Message = e.Message
                }
            };
        }
    }
}