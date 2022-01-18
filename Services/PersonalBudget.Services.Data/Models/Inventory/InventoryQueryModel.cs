namespace PersonalBudget.Services.Data.Models.Inventory
{
    using System.Collections.Generic;

    using PersonalBudget.Web.ViewModels.Inventory;

    public class InventoryQueryModel
    {
        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalProducts { get; set; }

        public IEnumerable<InventoryRecordViewModel> Products { get; set; }
    }
}
