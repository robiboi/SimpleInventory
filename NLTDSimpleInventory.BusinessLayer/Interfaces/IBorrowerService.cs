namespace NLTDSimpleInventory.BusinessLayer.Interfaces
{
    using NLTDSimpleInventory.DataLayer.Models;
    using System.Collections.Generic;

    public interface IBorrowerService
    {
        List<Borrower> GetAllBorrowers();
        IEnumerable<Borrower> SearchBorrowersByName(string query);
        Borrower AddNewBorrower(string name, string address);
    }
}
