namespace PersonalBudget.Web.ViewModels.Subcategories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllSubcategoriesViewModel : SearchViewModel
    {
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }

        public IEnumerable<SubcategoryViewModel> AllSubcategories { get; set; }
    }
}
