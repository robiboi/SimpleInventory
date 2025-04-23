using Microsoft.AspNetCore.Mvc;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
using NLTDSimpleInventory.Web.Models;

namespace NLTDSimpleInventory.Web.Controllers
{
    public class BorrowedController : Controller
    {
        private readonly IBorrowedService _borrowedItemService;

        public BorrowedController(
            IBorrowedService borrowedItemService)
        {
            _borrowedItemService = borrowedItemService;

        }

        public IActionResult Index()
        {
            var borrowedItems = _borrowedItemService.GetAllBorrowedItems();

            var sortedItems = borrowedItems
                .OrderBy(b => b.DateReturned.HasValue)      
                .ToList();

            var viewModel = sortedItems.Select(b => new BorrowedViewModel
            {
                Id = b.Id,
                ItemName = b.Item.Name,
                Borrower = b.Borrower.Name,
                DateBorrowed = b.DateBorrowed.ToString("yyyy-MM-dd"),
                DateReturnedRaw = b.DateReturned
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Borrow(BorrowItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill in the required fields correctly.";
                return RedirectToAction("Index", "Item");
            }

            int borrowerId;

            if (model.IsNewBorrower)
            {
                return RedirectToAction("Create", "Borrowers", new { model.NewBorrowerName, model.NewBorrowerAddress, model.ItemId, model.DateBorrowed});
            }

            if (!model.SelectedBorrowerId.HasValue)
            {
                TempData["Error"] = "Please select an existing borrower.";
                return RedirectToAction("Index", "Item");
            }

            borrowerId = model.SelectedBorrowerId.Value;

            _borrowedItemService.AddBorrowedItem(model.ItemId, borrowerId, model.DateBorrowed);
            _borrowedItemService.MarkItemAsUnavailable(model.ItemId);

            TempData["Success"] = "Item successfully borrowed.";
            return RedirectToAction("Index", "Item");
        }

        [HttpGet]
        public IActionResult Borrow(int itemId, int borrowerId, DateTime dateBorrowed)
        {
            if (itemId <= 0 || borrowerId <= 0)
            {
                TempData["Error"] = "Invalid borrow data.";
                return RedirectToAction("Index", "Item");
            }

            _borrowedItemService.AddBorrowedItem(itemId, borrowerId, dateBorrowed);
            _borrowedItemService.MarkItemAsUnavailable(itemId);

            TempData["Success"] = "Item successfully borrowed.";
            return RedirectToAction("Index", "Item");
        }

        public IActionResult ReturnItem(int id)
        {
            _borrowedItemService.ReturnBorrowedItem(id);
            TempData["ReturnSuccess"] = "Item returned successfully!";
            return RedirectToAction("Index");
        }
    }
}
