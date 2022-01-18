namespace PersonalBudget.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static PersonalBudget.Data.DataConstants.Item;

    public class CreateProductInputModel
    {
        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        [Display(Name = "Продукт/Услуга")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Изберете валидна стойност от списъка.")]
        [Display(Name = "Подкатегория")]
        public int SubCategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> SubCategories { get; set; }
    }
}
