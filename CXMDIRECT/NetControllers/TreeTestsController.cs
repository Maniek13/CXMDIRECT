using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Controllers;
using CXMDIRECT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public ActionResult AddTest()
        {
            int.TryParse(Request.Form["parrentId"], out int parrentId);
            string name = Request.Form["name"];
            string description = Request.Form["description"];

            var res = treeController.AddNode(parrentId, name, description);

            return View("Index", res);
        }

        [HttpPost]
        public ActionResult GetTest()
        {
            int.TryParse(Request.Form["id"], out int id);

            var res = treeController.GetNode(id);

            return View("Index", res);
        }

        public ActionResult EditTest()
        {
            int.TryParse(Request.Form["id"], out int id);
            int.TryParse(Request.Form["parrentId"], out int parrentId);
            string name = Request.Form["name"];
            string description = Request.Form["description"];

            var res = treeController.EditNode(id, parrentId, name, description);

            return View("Index", res);
        }
        public ActionResult DeleteTest()
        {
            int.TryParse(Request.Form["id"], out int id);

            var res = treeController.DeleteNode(id);

            return View("Index", res);
        }
    }
}