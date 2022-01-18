namespace PersonalBudget.Web.Areas.Reports.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Web.Areas.Reports.Models;
    using PersonalBudget.Web.Areas.Reports.Services;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Area("Reports")]
    [Authorize]
    public class IncomesBySubcategoriesController : Controller
    {
        private readonly IIncomesBySubcategoriesService incomesBySubcategoriesService;

        public IncomesBySubcategoriesController(IIncomesBySubcategoriesService incomesBySubcategoriesService)
        {
            this.incomesBySubcategoriesService = incomesBySubcategoriesService;
        }

        public IActionResult All(AllCostsViewModel allCostsView)
        {
            string userId = this.User.GetId();
            int itemsPerPage = 20;
            allCostsView.AllCosts = this.incomesBySubcategoriesService.GetAllIncomes(userId, allCostsView.InitialDate, allCostsView.FinalDate, allCostsView.PageNumber, itemsPerPage);
            return this.View(allCostsView);
        }
    }
}
