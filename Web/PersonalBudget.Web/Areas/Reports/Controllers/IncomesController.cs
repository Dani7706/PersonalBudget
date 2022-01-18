namespace PersonalBudget.Web.Areas.Reports.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.Areas.Reports.Models;
    using PersonalBudget.Web.Areas.Reports.Services;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Area("Reports")]
    [Authorize]
    public class IncomesController : Controller
    {
        private readonly IIncomesService incomesService;
        private readonly ISubcategoryService subcategoryService;

        public IncomesController(
            IIncomesService incomesService,
            ISubcategoryService subcategoryService)
        {
            this.incomesService = incomesService;
            this.subcategoryService = subcategoryService;
        }

        public IActionResult All([FromQuery] AllCostsViewModel allCostsView)
        {
            string userId = this.User.GetId();
            double totalCosts = 0;
            int itemsPerPage = 10;

            var incomes = new AllCostsViewModel()
            {
                ItemsPerPage = itemsPerPage,
                AllCosts = this.incomesService.GetAll(userId, allCostsView.SearchTerm, allCostsView.InitialDate, allCostsView.FinalDate, allCostsView.PageNumber, allCostsView.SubCategoryId, itemsPerPage),
                PageNumber = allCostsView.PageNumber,
                Count = this.incomesService.TotalCount(userId, allCostsView.SearchTerm, allCostsView.InitialDate, allCostsView.FinalDate, allCostsView.PageNumber, allCostsView.SubCategoryId, itemsPerPage),
                Subcategories = this.subcategoryService.GetAllSubCategories(userId),
            };
            totalCosts = incomes.AllCosts.Select(x => x.ItemValue).Sum();
            incomes.TotalCosts = totalCosts;
            return this.View(incomes);
        }
    }
}
