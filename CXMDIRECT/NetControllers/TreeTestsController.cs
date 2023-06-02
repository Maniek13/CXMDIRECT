using CXMDIRECT.Models;
using Microsoft.AspNetCore.Mvc;

namespace CXMDIRECT.NetControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TreeTestsController : Controller
    {
      
        TreeController treeController;

        public TreeTestsController()
        {
            treeController = new TreeController();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetTest()
        {
            bool isId = int.TryParse(Request.Form["id"], out int id);

            if(!isId)
            {
                return View("index", new Response<dynamic>()
                {
                    Data = new Error()
                    {
                        Message = "Please set id"
                    }
                });
            }

            var res = treeController.GetNode(id);

            return View("Index", res);
        }

        [HttpPost]
        public ActionResult AddTest()
        {
            bool isParrentId = int.TryParse(Request.Form["parrentId"], out int parrentId);
            string? name = Request.Form["name"];
            string? description = Request.Form["description"];

            if (!isParrentId)
            {
                return View("index", new Response<dynamic>()
                {
                    Data = new Error()
                    {
                        Message = "Please set parrent id"
                    }
                });
            }
            if (string.IsNullOrEmpty(name))
            {
                return View("index", new Response<dynamic>()
                {
                    Data = new Error()
                    {
                        Message = "Please set name"
                    }
                });
            }

            var res = treeController.AddNode(parrentId, name, description);

            return View("Index", res);
        }
        [HttpPost]
        public ActionResult EditTest()
        {
            bool isId = int.TryParse(Request.Form["id"], out int id);
            bool isParrentId = int.TryParse(Request.Form["parrentId"], out int parrentId);
            string? name = Request.Form["name"];
            string? description = Request.Form["description"];

            if (!isId)
            {
                return View("index", new Response<dynamic>()
                {
                    Data = new Error()
                    {
                        Message = "Please set id"
                    }
                });
            }
            if (!isParrentId)
            {
                return View("index", new Response<dynamic>()
                {
                    Data = new Error()
                    {
                        Message = "Please set parrent id"
                    }
                });
            }
            if (string.IsNullOrEmpty(name))
            {
                return View("index", new Response<dynamic>()
                {
                    Data = new Error()
                    {
                        Message = "Please set name"
                    }
                });
            }

            var res = treeController.EditNode(id, parrentId, name, description);

            return View("Index", res);
        }
        [HttpPost]
        public ActionResult DeleteTest()
        {
            bool isId = int.TryParse(Request.Form["id"], out int id);

            if (!isId)
            {
                return View("index", new Response<dynamic>()
                {
                    Data = new Error()
                    {
                        Message = "Please set id"
                    }
                });
            }

            var res = treeController.DeleteNode(id);

            return View("Index", res);
        }
    }
}