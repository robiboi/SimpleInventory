using Microsoft.AspNetCore.Mvc;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
using NLTDSimpleInventory.DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

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
            return View(items);
        }
    }
}