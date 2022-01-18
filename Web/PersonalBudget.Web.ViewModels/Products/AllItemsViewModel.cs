namespace PersonalBudget.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllItemsViewModel : PagingViewModel
    {
        [Display(Name = "Сортиране")]
        public int SubCategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> SubCategories { get; set; }

        [Display(Name = "Търсене")]
        public string SearchTerm { get; set; }

        public IEnumerable<ItemViewModel> Items { get; set; }
    }
}
