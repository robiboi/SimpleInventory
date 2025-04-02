using System.Collections.Generic;
using NLTDSimpleInventory.DataLayer.Models;

namespace NLTDSimpleInventory.BusinessLayer.Interfaces
{
    public interface IBorrowedService
    {
        List<BorrowedItem> GetAllBorrowedItems();
    }
}
