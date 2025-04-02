namespace NLTDSimpleInventory.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AddItemModel
    {
        public required string Name { get; set; }

        public required string Description { get; set; }
    }
}
