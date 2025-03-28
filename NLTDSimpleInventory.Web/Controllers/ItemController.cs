using Microsoft.AspNetCore.Mvc;
using NLTDSimpleInventory.BusinessLayer.Services;
using NLTDSimpleInventory.DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace NLTDSimpleInventory.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemService _itemService; 

        public ItemController(ItemService itemService) 
        {
            _itemService = itemService;
        }

        public IActionResult Index()
        {
            var items = _itemService.GetAllItems(); 
            return View(items);
        }
    }
}