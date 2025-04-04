namespace NLTDSimpleInventory.Web.Models
{
    public class BorrowedViewModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Borrower { get; set; }
        public string DateBorrowed { get; set; }
        public DateTime? DateReturnedRaw { get; set; }
        public string DateReturned => DateReturnedRaw?.ToString("yyyy-MM-dd") ?? "Not Returned";
    }
}
