using Microsoft.AspNetCore.Mvc;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
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
    }
}