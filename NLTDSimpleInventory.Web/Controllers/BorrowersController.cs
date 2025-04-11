using Microsoft.AspNetCore.Mvc;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
using NLTDSimpleInventory.Web.Models;

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

        [HttpGet]
        public IActionResult Create(string newBorrowerName, string newBorrowerAddress, int itemId, DateTime dateBorrowed)
        {
            if (string.IsNullOrWhiteSpace(newBorrowerName) || string.IsNullOrWhiteSpace(newBorrowerAddress))
            {
                TempData["Error"] = "Please provide the new borrower's name and address.";
                return RedirectToAction("Index", "Item");
            }

            var newBorrower = _borrowerService.AddNewBorrower(newBorrowerName, newBorrowerAddress);

            return RedirectToAction("Borrow", "Borrowed", new
            {
                itemId,
                borrowerId = newBorrower.Id,
                dateBorrowed = dateBorrowed.ToString("yyyy-MM-dd")
            });
        }

        [HttpGet]
        public IActionResult SearchBorrowers(string query)
        {
            var borrowers = _borrowerService.SearchBorrowersByName(query);

            var results = borrowers.Select(b => new BorrowerSearchResultViewModel
            {
                Id = b.Id,
                Name = b.Name
            }).ToList();

            return Json(results);
        }
    }
}
