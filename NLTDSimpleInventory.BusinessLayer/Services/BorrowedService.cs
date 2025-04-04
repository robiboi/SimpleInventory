﻿using System.Collections.Generic;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
using NLTDSimpleInventory.DataLayer.Models;

namespace NLTDSimpleInventory.BusinessLayer.Services
{
    public class BorrowedService : IBorrowedService
    {
        private readonly SimpleInventoryContext _context;

        public BorrowedService(SimpleInventoryContext context)
        {
            _context = context;
        }

        public List<BorrowedItem> GetAllBorrowedItems()
        {
            return _context.BorrowedItems
                .Select(b => new BorrowedItem
                {
                    Id = b.Id,
                    Item = b.Item,
                    Borrower = b.Borrower,
                    DateBorrowed = b.DateBorrowed,
                    DateReturned = b.DateReturned
                })
                .ToList();
        }
    }
}
