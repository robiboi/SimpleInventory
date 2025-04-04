using Microsoft.AspNetCore.Mvc;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
using NLTDSimpleInventory.DataLayer.Models;
using NLTDSimpleInventory.Web.Models;


namespace NLTDSimpleInventory.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        public IActionResult Index()
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

        [HttpPost]
        public IActionResult AddItem(AddItemModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var newItem = new Item
            {
                Name = model.Name,
                Description = model.Description,
                ItemSKU = Guid.NewGuid().ToString(),
                DateAdded = DateTime.UtcNow,
                IsAvailable = true
            };

            _itemService.AddItem(newItem);
            TempData["SuccessMessage"] = "Item added successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult EditItem(int id)
        {
            var item = _itemService.GetAllItems().FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();  
            }

            var editModel = new AddItemModel
            {
                Name = item.Name,
                Description = item.Description
            };

            return PartialView("_EditItemModal", editModel);   
        }

        [HttpPost]
        public IActionResult EditItem(int id, AddItemModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input.");
            }

            var item = _itemService.GetAllItems().FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.Name = model.Name;
            item.Description = model.Description;
            item.UpdatedAt = DateTime.UtcNow;

            _itemService.UpdateItem(item);

            TempData["SuccessMessage"] = "Item updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ArchiveItem(int id)
        {
            try
            {
                _itemService.ArchiveItem(id);
                TempData["SuccessMessage"] = "Item archived successfully!";
            }
            catch (InvalidOperationException)
            {
                TempData["ErrorMessage"] = "Item not found!";
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}