﻿namespace NLTDSimpleInventory.BusinessLayer.Interfaces
{
    using NLTDSimpleInventory.DataLayer.Models;
    using System.Collections.Generic;

    public interface IBorrowerService
    {
        List<Borrower> GetAllBorrowers();
    }
}
