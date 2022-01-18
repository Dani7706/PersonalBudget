namespace PersonalBudget.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;

    using static PersonalBudget.Data.DataConstants.Category;

    public class CreateCategoryInputModel
    {
        [Required(ErrorMessage = "Въведете име на категория")]
        [StringLength(MaxNameLength, ErrorMessage = "Категорията трябва да е между {2} и {1} символа", MinimumLength = MinNameLength)]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Изберете валиден тип")]
        [Display(Name = "Тип")]
        public ItemType ItemTypes { get; set; }
    }
}
