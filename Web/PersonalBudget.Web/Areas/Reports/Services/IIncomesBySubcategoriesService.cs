namespace PersonalBudget.Web.Areas.Reports.Services
{
    using System.Collections.Generic;

    using PersonalBudget.Web.Areas.Reports.Models;

    public interface IIncomesBySubcategoriesService
    {
        List<CostViewModel> GetAllIncomes(string userId, string initialDate, string finalDate, int page, int itemPerPage = 5);
    }
}
