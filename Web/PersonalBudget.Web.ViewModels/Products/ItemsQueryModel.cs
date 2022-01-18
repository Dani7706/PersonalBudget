namespace PersonalBudget.Web.ViewModels.Products
{
    using System.Collections.Generic;

    public class ItemsQueryModel
    {
        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalItems { get; set; }

        public IEnumerable<ItemViewModel> Items { get; set; }
    }
}
