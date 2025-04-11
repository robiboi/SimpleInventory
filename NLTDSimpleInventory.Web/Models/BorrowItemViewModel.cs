public class BorrowItemViewModel
{
    public int ItemId { get; set; }
    public int? SelectedBorrowerId { get; set; }

    public bool IsNewBorrower { get; set; }
    public string? NewBorrowerName { get; set; }
    public string? NewBorrowerAddress { get; set; }

    public DateTime DateBorrowed { get; set; } = DateTime.Now;
}
