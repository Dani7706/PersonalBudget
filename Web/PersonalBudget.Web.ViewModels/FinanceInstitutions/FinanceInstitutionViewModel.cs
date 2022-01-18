namespace PersonalBudget.Web.ViewModels.FinanceInstitutions
{
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    public class FinanceInstitutionViewModel : IMapFrom<FinanceInstitution>
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Финансов тип")]
        public string FinanceTypeName { get; set; }

        [Display(Name = "Капитал")]
        public string Capital { get; set; }
    }
}
