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
        public ActionResult GetTest([FromForm]int? id)
        {
            if(id == null)
            {
                return View("index", new Response<dynamic>()
                {
                    Data = new Error()
                    {
                        Message = "Please set id"
                    }
                });
            }

            var res = treeController.GetNode((int)id);

            return View("Index", res);
        }

        [HttpPost]
        public ActionResult AddTest([FromForm]int? parrentId, [FromForm]string? name, [FromForm]string? description)
        {
            if (parrentId == null)
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

            var res = treeController.AddNode((int)parrentId, name, description);

            return View("Index", res);
        }
        [HttpPost]
        public ActionResult EditTest([FromForm]int? id, [FromForm]int? parrentId, [FromForm]string? name, [FromForm]string? description)
        {
            if (id == null)
            {
                return View("index", new Response<dynamic>()
                {
                    Data = new Error()
                    {
                        Message = "Please set id"
                    }
                });
            }
            if (parrentId == null)
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

            var res = treeController.EditNode((int)id, (int)parrentId, name, description);

            return View("Index", res);
        }
        [HttpPost]
        public ActionResult DeleteTest([FromForm]int? id)
        {
            if (id == null)
            {
                return View("index", new Response<dynamic>()
                {
                    Data = new Error()
                    {
                        Message = "Please set id"
                    }
                });
            }

            var res = treeController.DeleteNode((int)id);

            return View("Index", res);
        }
    }
}