using Microsoft.AspNetCore.Mvc;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
using NLTDSimpleInventory.Web.Models;


namespace NLTDSimpleInventory.Web.Controllers
{
    public class BorrowedController : Controller
    {
        private readonly IBorrowedService _borrowedItemService;

        public BorrowedController(IBorrowedService borrowedItemService)
        {
            _borrowedItemService = borrowedItemService;
        }

        public IActionResult Index()
        {
            var borrowedItems = _borrowedItemService.GetAllBorrowedItems();

            var viewModel = borrowedItems.Select(b => new BorrowedViewModel
            {
                Id = b.Id,
                ItemName = b.Item?.Name,
                Borrower = b.Borrower?.Name,
                DateBorrowed = b.DateBorrowed.ToString("yyyy-MM-dd"),
                DateReturnedRaw = b.DateReturned
            }).ToList();

            return View(viewModel);
        }
    }
}
