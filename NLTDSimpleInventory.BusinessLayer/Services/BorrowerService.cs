namespace NLTDSimpleInventory.BusinessLayer.Services
{
    using Microsoft.EntityFrameworkCore;
    using NLTDSimpleInventory.BusinessLayer.Interfaces;
    using NLTDSimpleInventory.DataLayer.Models;
    using System.Collections.Generic;
    

    public class BorrowerService : IBorrowerService
    {
        private readonly SimpleInventoryContext _context;

        public BorrowerService(SimpleInventoryContext context)
        {
            _context = context;
        }

        public List<Borrower> GetAllBorrowers()
        {
            return _context.Borrowers
                .Include(b => b.BorrowedItems)
                    .ThenInclude(bi => bi.Item) 
                .ToList();
        }
    }
}
