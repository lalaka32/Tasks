using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProxy.Helper;

namespace WebProxy.Controllers
{
    //[Route("")]
    public class WebProxyController : Controller
    {
        private readonly IWebProxyService _webProxyService;

        public WebProxyController(IWebProxyService webProxyService)
        {
            _webProxyService = webProxyService;
        }

        [HttpGet("{**route}")]
        public async Task<IActionResult> Index(string route)
        {
            var page = await _webProxyService.GetResource(route);

            ViewBag.Page = HtmlHelperService.InsertCharAfterEveryWordWithNCharsInContent(page, '™', 6);

            return View();
        }
    }
}