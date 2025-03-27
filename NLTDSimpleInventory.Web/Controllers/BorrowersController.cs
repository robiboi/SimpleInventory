using Microsoft.AspNetCore.Mvc;

namespace NLTDSimpleInventory.Web.Controllers
{
    public class BorrowersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
