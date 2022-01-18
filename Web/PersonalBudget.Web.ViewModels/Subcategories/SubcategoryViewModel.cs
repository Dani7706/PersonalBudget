namespace PersonalBudget.Web.ViewModels.Subcategories
{
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    using static PersonalBudget.Data.DataConstants.SubCategory;

    public class SubcategoryViewModel : IMapFrom<SubCategory>
    {
        public int Id { get; set; }

        [Display(Name = "Подкатегория")]
        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Display(Name = "Категория")]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string CategoryName { get; set; }
    }
}
