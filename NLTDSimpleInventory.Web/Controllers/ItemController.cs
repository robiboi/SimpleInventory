using Microsoft.AspNetCore.Mvc;

namespace NLTDSimpleInventory.Web.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
