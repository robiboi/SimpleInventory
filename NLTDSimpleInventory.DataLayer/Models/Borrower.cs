using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLTDSimpleInventory.DataLayer.Models
{
    public class Borrower
    {
        public int Id { get; set; }  

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }  

        [Required]
        [StringLength(255)]
        public required string Address { get; set; }
        public required DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
