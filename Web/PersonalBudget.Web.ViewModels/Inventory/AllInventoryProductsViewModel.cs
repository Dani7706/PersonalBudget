namespace PersonalBudget.Web.ViewModels.Inventory
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllInventoryProductsViewModel : SearchViewModel
    {
        public IEnumerable<KeyValuePair<string, string>> SubCategories { get; set; }

        public IEnumerable<InventoryRecordViewModel> Products { get; set; }
    }
}
