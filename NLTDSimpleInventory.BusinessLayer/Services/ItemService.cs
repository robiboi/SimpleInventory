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
            return _context.Items.Where(i => i.ArchivedDate == null).ToList();
        }

        public void AddItem(Item item)
        {
            item.ItemSKU = GenerateSKU(item.Name);
            item.DateAdded = DateTime.Now;
            item.IsAvailable = true;

            _context.Items.Add(item);   
            _context.SaveChanges();
        }

        public void UpdateItem(Item item)
        {
            var existingItem = _context.Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
            {
                throw new InvalidOperationException("Item not found.");
            }

            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.UpdatedAt = DateTime.Now;

            _context.Items.Update(existingItem);
            _context.SaveChanges();
        }

        public void ArchiveItem(int id)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                throw new InvalidOperationException("Item not found.");
            }

            item.ArchivedDate = DateTime.Now; 
            _context.Items.Update(item);
            _context.SaveChanges();
        }

        private string GenerateSKU(string name)
        {
            string prefix = name.Length >= 3 ? name.Substring(0, 3).ToUpper() : "SKU";
            return $"{prefix}-{Guid.NewGuid().ToString("N").Substring(0, 5).ToUpper()}";
        }
    }
}
