namespace NLTDSimpleInventory.Web.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public required string ItemSKU { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public bool IsAvailable { get; set; }
        public string AvailabilityStatus => IsAvailable ? "Available" : "Borrowed";
    }
}
