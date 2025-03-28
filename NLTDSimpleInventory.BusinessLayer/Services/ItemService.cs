namespace NLTDSimpleInventory.BusinessLayer.Services
{
    using NLTDSimpleInventory.DataLayer.Models;
    using NLTDSimpleInventory.DataLayer;
    using System.Collections.Generic;
    using System.Linq;

    public class ItemService
    {
        private readonly SimpleInventoryContext _context;

        public ItemService(SimpleInventoryContext context)
        {
            _context = context;
        }

        public List<Item> GetAllItems()
        {
            return _context.Items.ToList(); 
        }
    }
}
