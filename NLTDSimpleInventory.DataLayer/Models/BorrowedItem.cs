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

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        [ForeignKey("Borrower")]        
        public int BorrowerId { get; set; }

        public DateTime DateBorrowed { get; set; } = DateTime.UtcNow;
        public DateTime? DateReturned { get; set; }


        public required Item Item { get; set; }
        public required Borrower Borrower { get; set; }
    }
}
