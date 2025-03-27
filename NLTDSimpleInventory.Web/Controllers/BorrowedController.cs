using Microsoft.AspNetCore.Mvc;

namespace NLTDSimpleInventory.Web.Controllers
{
    public class BorrowedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
