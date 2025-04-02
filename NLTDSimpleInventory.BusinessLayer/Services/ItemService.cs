namespace NLTDSimpleInventory.BusinessLayer.Services
{
    using NLTDSimpleInventory.BusinessLayer.Interfaces;
    using NLTDSimpleInventory.DataLayer.Models;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Linq;

    public class ItemService : IItemService
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

        public void AddItem(Item item)
        {
            item.ItemSKU = GenerateSKU(item.Name);
            item.DateAdded = DateTime.Now;
            item.IsAvailable = true;

            _context.Items.Add(item);
            _context.SaveChanges();
        }

        private string GenerateSKU(string name)
        {
            string prefix = name.Length >= 3 ? name.Substring(0, 3).ToUpper() : "SKU";
            return $"{prefix}-{Guid.NewGuid().ToString("N").Substring(0, 5).ToUpper()}";
        }
    }
}
