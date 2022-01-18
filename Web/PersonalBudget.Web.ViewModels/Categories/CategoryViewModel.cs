namespace PersonalBudget.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    using static PersonalBudget.Data.DataConstants.Category;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Въведете име на категория")]
        [StringLength(MaxNameLength, ErrorMessage = "Категорията трябва да е между {2} и {1} символа", MinimumLength = MinNameLength)]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        [Display(Name = "Категория")]
        public string Name { get; set; }

        [Display(Name = "Тип")]
        public string ItemType { get; set; }
    }
}
