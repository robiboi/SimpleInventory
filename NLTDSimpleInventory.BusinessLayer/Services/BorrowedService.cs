using System.Collections.Generic;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
using NLTDSimpleInventory.DataLayer.Models;

namespace NLTDSimpleInventory.BusinessLayer.Services
{
    public class BorrowedService : IBorrowedService
    {
        private readonly SimpleInventoryContext _context;

        public BorrowedService(SimpleInventoryContext context)
        {
            _context = context;
        }

        public List<BorrowedItem> GetAllBorrowedItems()
        {
            return _context.BorrowedItems
                .Select(b => new BorrowedItem
                {
                    Id = b.Id,
                    Item = b.Item,
                    Borrower = b.Borrower,
                    DateBorrowed = b.DateBorrowed,
                    DateReturned = b.DateReturned
                })
                .ToList();
        }

        public void AddBorrowedItem(int itemId, int borrowerId, DateTime dateBorrowed)
        {
            var borrowed = new BorrowedItem
            {
                ItemId = itemId,
                BorrowerId = borrowerId,
                DateBorrowed = dateBorrowed
            };
            _context.BorrowedItems.Add(borrowed);
            _context.SaveChanges();
        }

        public void MarkItemAsUnavailable(int itemId)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                item.IsAvailable = false;  
                _context.SaveChanges();
            }
        }
        
        public void ReturnBorrowedItem(int id)
        {
            var borrowedItem = _context.BorrowedItems.FirstOrDefault(b => b.Id == id);
            if (borrowedItem != null && borrowedItem.DateReturned == null)
            {
                borrowedItem.DateReturned = DateTime.UtcNow;

                var item = _context.Items.FirstOrDefault(i => i.Id == borrowedItem.ItemId);
                if (item != null)
                {
                    item.IsAvailable = true;
                }

                _context.SaveChanges();
            }
        }
    }
}
