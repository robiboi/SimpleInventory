namespace NLTDSimpleInventory.Web.Models
{
    public class BorrowersViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public List<BorrowedItemHistoryViewModel> BorrowHistory { get; set; } = new();

    }

    public class BorrowedItemHistoryViewModel
    {
        public string ItemName { get; set; }
        public string ItemSKU { get; set; }
        public string DateBorrowed { get; set; }
        public string DateReturned { get; set; }
    }
}
