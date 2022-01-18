namespace PersonalBudget.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    public class AllCategoriesViewModel : PagingViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
