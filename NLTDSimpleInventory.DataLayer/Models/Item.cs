using System.ComponentModel.DataAnnotations;

namespace NLTDSimpleInventory.DataLayer.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [StringLength(50)]
        public required string ItemSKU { get; set; }

        [StringLength(255)]
        public required string Description { get; set; }

        public bool IsAvailable { get; set; }
        public required DateTime DateAdded { get; set; }
        public required DateTime UpdatedAt { get; set; }
        public DateTime? ArchivedDate { get; set; }

        public List<BorrowedItem> BorrowedItems { get; set; } = [];
    }
}
