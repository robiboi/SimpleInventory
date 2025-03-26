using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLTDSimpleInventory.DataLayer.Models
{
    public class BorrowedItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int BorrowerId { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}
