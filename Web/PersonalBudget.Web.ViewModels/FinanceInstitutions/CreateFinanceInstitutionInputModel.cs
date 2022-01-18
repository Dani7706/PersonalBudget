namespace PersonalBudget.Web.ViewModels.FinanceInstitutions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static PersonalBudget.Data.DataConstants.FinanceInstitution;

    public class CreateFinanceInstitutionInputModel
    {
        [Required(ErrorMessage = "Въведете име на град")]
        [StringLength(MaxNameLength, ErrorMessage = "Градът трябва да е между {2} и {1} символа", MinimumLength = MinNameLength)]
        [MinLength(MinNameLength)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Изберете валиден тип")]
        [Display(Name = "Тип")]
        public int FinanceTypeId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> FinanceTypes { get; set; }

        [Required(ErrorMessage = "Въведете начална наличност.")]
        [Range(MinCapitalValue, MaxCapitalValue, ErrorMessage = "Началната наличност трябва да е между {1} и {2}")]
        [Display(Name = "Начална наличност")]
        public string Capital { get; set; }
    }
}
