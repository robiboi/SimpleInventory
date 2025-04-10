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

        public void UpdateBorrower(Borrower updatedBorrower)
        {
            var borrower = _context.Borrowers.FirstOrDefault(b => b.Id == updatedBorrower.Id);
            if (borrower == null)
                throw new InvalidOperationException("Borrower not found.");

            borrower.Name = updatedBorrower.Name;
            borrower.Address = updatedBorrower.Address;

            _context.Borrowers.Update(borrower);
            _context.SaveChanges();
        }
    }
}
