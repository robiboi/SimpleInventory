namespace NLTDSimpleInventory.BusinessLayer.Interfaces
{
    using NLTDSimpleInventory.DataLayer.Models;
    using System.Collections.Generic;

    public interface IItemService
    {
        List<Item> GetAllItems();
        void AddItem(Item item);
        void UpdateItem(Item item);
    }
}
