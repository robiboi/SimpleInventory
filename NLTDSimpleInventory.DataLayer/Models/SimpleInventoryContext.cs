using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NLTDSimpleInventory.DataLayer.Models
{
    public class SimpleInventoryContext : DbContext
    {
        public SimpleInventoryContext(DbContextOptions<SimpleInventoryContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BorrowedItem> BorrowedItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowedItem>()
                .HasOne(b => b.Item)
                .WithMany(b => b.BorrowedItems)
                .HasForeignKey(b => b.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BorrowedItem>()
                .HasOne(b => b.Borrower)
                .WithMany(b => b.BorrowedItems)
                .HasForeignKey(b => b.BorrowerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
