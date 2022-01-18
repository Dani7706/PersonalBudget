namespace PersonalBudget.Web.Areas.Reports.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.Areas.Reports.Models;
    using PersonalBudget.Web.Areas.Reports.Services;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Area("Reports")]
    [Authorize]
    public class CostsController : Controller
    {
        private readonly ITransferService transferService;
        private readonly ICostsService costsService;
        private readonly ISubcategoryService subcategoryService;

        public CostsController(
            ITransferService transferService,
            ICostsService costsService,
            ISubcategoryService subcategoryService)
        {
            this.transferService = transferService;
            this.costsService = costsService;
            this.subcategoryService = subcategoryService;
        }

        public IActionResult All([FromQuery] AllCostsViewModel allCostsView)
        {
            string userId = this.User.GetId();
            double totalCosts = 0;
            int itemsPerPage = allCostsView.ItemsPerPage;
            if (itemsPerPage == 0)
            {
                itemsPerPage = 10;
            }

            var costs = this.costsService.GetAll(userId, allCostsView.SearchTerm, allCostsView.InitialDate, allCostsView.FinalDate, allCostsView.PageNumber, allCostsView.SubCategoryId, itemsPerPage);
            allCostsView.ItemsPerPage = itemsPerPage;
            allCostsView.AllCosts = costs;
            allCostsView.Count = this.costsService.TotalCount(userId, allCostsView.SearchTerm, allCostsView.InitialDate, allCostsView.FinalDate, allCostsView.PageNumber, allCostsView.SubCategoryId, itemsPerPage);
            allCostsView.Subcategories = this.subcategoryService.GetAllSubCategories(userId);

            totalCosts = costs.Select(x => x.ItemValue).Sum();
            totalCosts = Math.Round(totalCosts, 2);
            allCostsView.TotalCosts =totalCosts;
            return this.View(allCostsView);
        }
    }
}
