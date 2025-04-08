using Microsoft.AspNetCore.Mvc;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
using NLTDSimpleInventory.BusinessLayer.Services;
using NLTDSimpleInventory.DataLayer.Models;
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

        // GET: Show the edit form with current borrower data
        public IActionResult EditBorrower(int id)
        {
            var borrower = _borrowerService.GetAllBorrowers().FirstOrDefault(b => b.Id == id);
            if (borrower == null)
            {
                return NotFound();
            }

            var model = new BorrowerModel
            {
                Name = borrower.Name,
                Address = borrower.Address
            };

            ViewData["BorrowerId"] = borrower.Id; // Optional, if needed in view
            return PartialView("_EditBorrowerModal", model);
        }

        // POST: Update borrower details
        [HttpPost]
        public IActionResult EditBorrower(int id, BorrowerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input.");
            }

            var borrower = _borrowerService.GetAllBorrowers().FirstOrDefault(b => b.Id == id);
            if (borrower == null)
            {
                return NotFound();
            }

            borrower.Name = model.Name;
            borrower.Address = model.Address;

            _borrowerService.UpdateBorrower(borrower);

            TempData["BorrowerUpdateMsg"] = "Borrower updated successfully!";
            return RedirectToAction("Index");
        }

    }
}
