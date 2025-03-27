using Microsoft.AspNetCore.Mvc;
using NLTDSimpleInventory.DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace NLTDSimpleInventory.Controllers
{
    public class ItemController : Controller
    {
        private readonly SimpleInventoryContext _context;

        public ItemController(SimpleInventoryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _context.Items.ToList(); 
            return View(items);
        }
    }
}
