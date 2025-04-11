using NLTDSimpleInventory.DataLayer.Models;


namespace NLTDSimpleInventory.BusinessLayer.Interfaces
{
    public interface IBorrowedService
    {
        List<BorrowedItem> GetAllBorrowedItems();
        void AddBorrowedItem(int itemId, int borrowerId, DateTime dateBorrowed);
        void MarkItemAsUnavailable(int itemId);
        void ReturnBorrowedItem(int id);
    }
}
