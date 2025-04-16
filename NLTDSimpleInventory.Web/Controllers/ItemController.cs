using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
using NLTDSimpleInventory.DataLayer.Models;
using NLTDSimpleInventory.Web.Models;


namespace NLTDSimpleInventory.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemService itemService, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var items = _itemService.GetAllItems();

            var viewModel = items.Select(item => new ItemViewModel
            {
                Id = item.Id,
                ItemSKU = item.ItemSKU,
                Name = item.Name,
                Description = item.Description,
                IsAvailable = item.IsAvailable
            }).ToList();

            return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving items list: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                TempData["Error"] = "An unexpected error occurred while retrieving item data.";
                throw;
            }
        }

        [HttpPost]
        public IActionResult AddItem(ItemModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid item data. Please check required fields.";
                return RedirectToAction("Index");
            }
            try
            {
            var newItem = new Item
            {
                Name = model.Name,
                Description = model.Description,
                ItemSKU = Guid.NewGuid().ToString(),
                DateAdded = DateTime.UtcNow,
                IsAvailable = true
            };

            _itemService.AddItem(newItem);
            TempData["Success"] = "Item added successfully!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding item: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                TempData["Error"] = "An unexpected error occurred while adding the item.";
            }
            return RedirectToAction("Index");
        }

        // GET: Show the edit form with current item data
        public IActionResult EditItem(int id)
        {
            try
            {
            var item = _itemService.GetAllItems().FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();  
            }

            var editModel = new ItemModel
            {
                Name = item.Name,
                Description = item.Description
            };

            return PartialView("_EditItemModal", editModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving item for editing with ID {ItemId}: {Message}\nStackTrace: {StackTrace}", id, ex.Message, ex.StackTrace);
                TempData["Error"] = "An unexpected error occurred while loading item data.";
                return RedirectToAction("Index");
            }
        }

        // POST: Update item details
        [HttpPost]
        public IActionResult EditItem(int id, ItemModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid item data.";
                return RedirectToAction("Index");
            }

            try
            {
            var item = _itemService.GetAllItems().FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                    TempData["Error"] = "Item not found.";
                    return RedirectToAction("Index");
            }

                if (item.Name == model.Name && item.Description == model.Description)
                {
                    return RedirectToAction("Index");
                }


                item.Name = model.Name;
            item.Description = model.Description;
            item.UpdatedAt = DateTime.UtcNow;

            _itemService.UpdateItem(item);

            TempData["Success"] = "Item updated successfully!";
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating item: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                TempData["Error"] = "An unexpected error occurred while updating the item.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ArchiveItem(int id)
        {
            try
            {
                _itemService.ArchiveItem(id);
                TempData["Success"] = "Item archived successfully!";
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Archive failed: Item not found with ID {Id}", id);
                TempData["Error"] = "Item not found!";
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error archiving item: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                TempData["Error"] = "An unexpected error occurred while archiving the item.";
            }

            return RedirectToAction("Index");
        }
    }
}