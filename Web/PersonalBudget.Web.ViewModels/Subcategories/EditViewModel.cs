namespace PersonalBudget.Web.ViewModels.Subcategories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditViewModel : SubcategoryViewModel
    {
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
    }
}
