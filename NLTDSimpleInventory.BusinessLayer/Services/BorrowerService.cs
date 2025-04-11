namespace NLTDSimpleInventory.BusinessLayer.Services
{
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
            return _context.Borrowers.ToList();
        }

        public IEnumerable<Borrower> SearchBorrowersByName(string query)
        {
            return _context.Borrowers
                .Where(b => b.Name.Contains(query))
                .ToList();
        }

        public Borrower AddNewBorrower(string name, string address)
        {
            var borrower = new Borrower
            {
                Name = name,
                Address = address,
                DateAdded = DateTime.Now
            };

            _context.Borrowers.Add(borrower);
            _context.SaveChanges();

            return borrower;
        }

    }
}
