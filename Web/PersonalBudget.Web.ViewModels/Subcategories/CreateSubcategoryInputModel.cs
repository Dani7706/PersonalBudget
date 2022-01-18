namespace PersonalBudget.Web.ViewModels.Subcategories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static PersonalBudget.Data.DataConstants.SubCategory;

    public class CreateSubcategoryInputModel
    {
        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        [Display(Name = "Подкатегория")]
        public string Name { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
    }
}
