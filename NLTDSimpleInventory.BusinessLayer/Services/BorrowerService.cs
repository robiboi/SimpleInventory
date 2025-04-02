namespace NLTDSimpleInventory.BusinessLayer.Services
{
    using NLTDSimpleInventory.BusinessLayer.Interfaces;
    using NLTDSimpleInventory.DataLayer.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class BorrowerService : IBorrowerService
    {
        private readonly SimpleInventoryContext _context;

        public BorrowerService(SimpleInventoryContext context)
        {
            _context = context;
        }

        public List<Borrower> GetAllBorrowers()
        {
            return _context.Borrowers.ToList();
        }
    }
}
