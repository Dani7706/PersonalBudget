namespace PersonalBudget.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using PersonalBudget.Web.ViewModels.FinanceInstitutions;

    public class HomePageViewModel
    {
        public decimal AllIncomes { get; set; }

        public decimal AllCosts { get; set; }

        public IEnumerable<FinanceInstitutionViewModel> FinanceInstitutions { get; set; }
    }
}
