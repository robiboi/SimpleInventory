namespace NLTDSimpleInventory.DataLayer.Models
{
    public class BorrowedItem
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public required Item Item { get; set; }

        public int BorrowerId { get; set; }
        public required Borrower Borrower { get; set; }

        public DateTime DateBorrowed { get; set; } = DateTime.UtcNow;
        public DateTime? DateReturned { get; set; }

    }
}
