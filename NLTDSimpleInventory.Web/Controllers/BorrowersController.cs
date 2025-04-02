using Microsoft.AspNetCore.Mvc;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
using NLTDSimpleInventory.Web.Models;
using System.Linq;

namespace NLTDSimpleInventory.Web.Controllers
{
    public class BorrowersController : Controller
    {
        private readonly IBorrowerService _borrowerService;

        public BorrowersController(IBorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }

        public IActionResult Index()
        {
            var borrowers = _borrowerService.GetAllBorrowers();

            var viewModel = borrowers.Select(b => new BorrowersViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Address = b.Address
            }).ToList();

            return View(viewModel);
        }
    }
}
