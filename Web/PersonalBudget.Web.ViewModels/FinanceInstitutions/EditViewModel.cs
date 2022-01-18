namespace PersonalBudget.Web.ViewModels.FinanceInstitutions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditViewModel : FinanceInstitutionViewModel
    {
        [Required]
        [Display(Name = "Тип")]
        public int FinanceTypeId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> FinanceTypes { get; set; }
    }
}
