using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLTDSimpleInventory.DataLayer.Models
{
    public class BorrowedItem
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;
        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; } = null!;

        public DateTime DateBorrowed { get; set; } = DateTime.UtcNow;
        public DateTime? DateReturned { get; set; }
    }
}
