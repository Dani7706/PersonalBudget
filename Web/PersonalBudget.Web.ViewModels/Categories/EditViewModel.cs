namespace PersonalBudget.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;

    public class EditViewModel : CategoryViewModel
    {
        [Required(ErrorMessage = "Изберете валиден тип")]
        [Display(Name = "Тип")]
        public ItemType ItemTypes { get; set; }
    }
}
