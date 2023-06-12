using CXMDIRECT.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CXMDIRECT.NetControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TreeTestsController : Controller
    {
        readonly TreeController treeController;
        private readonly IMemoryCache _memoryCache;

  
        public TreeTestsController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            treeController = new TreeController(memoryCache);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetTest([FromForm]int? id)
        {

            if(id == null)
            {
                return View("index", new Response<dynamic>("GetTest", -1, new Error("Please set id")));
            }

            var res = await treeController.GetNode((int)id);

            return View("Index", res.Value);
        }

        [HttpPost]
        public async Task<ActionResult> AddTest([FromForm]int? parrentId, [FromForm]string? name, [FromForm]string? description)
        {
            if (string.IsNullOrEmpty(name))
            {
                return View("index", new Response<dynamic>("AddTest", -1, new Error("Please set name")));
            }

            var res = await treeController.AddNode(parrentId, name, description);

            return View("Index", res.Value);
        }

        [HttpPost]
        public async Task<ActionResult> EditTest([FromForm]int? id, [FromForm]int? parrentId, [FromForm]string? name, [FromForm]string? description)
        {
            if (id == null)
            {
                return View("index", new Response<dynamic>("EditTest", -1, new Error("Please set id")));
            }
            if (parrentId == null)
            {
                return View("index", new Response<dynamic>("EditTest", -1, new Error("Please set parrent id")));
            }
            if (string.IsNullOrEmpty(name))
            {
                return View("index", new Response<dynamic>("EditTest", -1, new Error("Please set name")));
            }

            var res = await treeController.EditNode((int)id, (int)parrentId, name, description);

            return View("Index", res.Value);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteTest([FromForm]int? id)
        {
            if (id == null)
            {
                return View("index", new Response<dynamic>("DeleteTest", -1, new Error("Please set id")));
            }

            var res = await treeController.DeleteNode((int)id);

            return View("Index", res.Value);
        }
    }
}