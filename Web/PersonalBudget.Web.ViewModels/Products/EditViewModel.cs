namespace PersonalBudget.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    public class EditViewModel : IMapFrom<Item>
    {
        [Required]
        [Display(Name = "Продукт/Услуга")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Подкатегория")]
        public int SubCategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> SubCategories { get; set; }
    }
}
