using Microsoft.AspNetCore.Mvc;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
using NLTDSimpleInventory.BusinessLayer.Services;
using NLTDSimpleInventory.DataLayer.Models;
using NLTDSimpleInventory.Web.Models;
using Microsoft.Extensions.Logging;

namespace NLTDSimpleInventory.Web.Controllers
{
    public class BorrowersController : Controller
    {
        private readonly IBorrowerService _borrowerService;
        private readonly ILogger<BorrowersController> _logger;

        public BorrowersController(IBorrowerService borrowerService, ILogger<BorrowersController> logger)
        {
            _borrowerService = borrowerService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
            var borrowers = _borrowerService.GetAllBorrowers();
            var viewModel = borrowers.Select(b => new BorrowersViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Address = b.Address,
                BorrowHistory = b.BorrowedItems.Select(bi => new BorrowedItemHistoryViewModel
                {
                    ItemName = bi.Item.Name,
                    ItemSKU = bi.Item.ItemSKU,
                    DateBorrowed = bi.DateBorrowed.ToString("yyyy-MM-dd"),
                    DateReturned = bi.DateReturned?.ToString("yyyy-MM-dd") ?? "Not Returned"
                }).ToList()
            }).ToList();

            return View(viewModel);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving borrowers list: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                TempData["Error"] = "An unexpected error occurred while retrieving borrower data.";
                throw;
            }
        }

        [HttpGet]
        public IActionResult Create(string newBorrowerName, string newBorrowerAddress, int itemId, DateTime dateBorrowed)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new borrower: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                TempData["Error"] = "An unexpected error occurred while creating the borrower.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult SearchBorrowers(string query)
        {
            try
            {
            var borrowers = _borrowerService.SearchBorrowersByName(query);

            var results = borrowers.Select(b => new BorrowerSearchResultViewModel
            {
                Id = b.Id,
                Name = b.Name
            }).ToList();

            return Json(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching borrowers: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                TempData["Error"] = "An unexpected error occurred while searching for borrowers.";
                return Json(new { success = false, message = "Error occurred while searching." });
            }

        }

        // GET: Show the edit form with current borrower data
        public IActionResult EditBorrower(int id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving borrower for editing with ID {BorrowerId}: {Message}\nStackTrace: {StackTrace}", id, ex.Message, ex.StackTrace);
                TempData["Error"] = "An unexpected error occurred while loading borrower data.";
                return RedirectToAction("Index");
            }
        }

        // POST: Update borrower details
        [HttpPost]
        public IActionResult EditBorrower(int id, BorrowerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input.");
            }
            try
            {

            var borrower = _borrowerService.GetAllBorrowers().FirstOrDefault(b => b.Id == id);
            if (borrower == null)
            {
                return NotFound();
            }

            borrower.Name = model.Name;
            borrower.Address = model.Address;

            _borrowerService.UpdateBorrower(borrower);

            TempData["Success"] = "Borrower updated successfully!";
            return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating borrower with ID {BorrowerId}: {Message}\nStackTrace: {StackTrace}", id, ex.Message, ex.StackTrace);
                TempData["Error"] = "An unexpected error occurred while updating the borrower.";
                throw;
            }
        }
    }
}
