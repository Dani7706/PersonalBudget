namespace PersonalBudget.Web.ViewModels.FinanceInstitutions
{
    using System.Collections.Generic;

    public class AllFinanceInstitutionsViewModel
    {
        public IEnumerable<FinanceInstitutionViewModel> FinanceInstitutions { get; set; }
    }
}
