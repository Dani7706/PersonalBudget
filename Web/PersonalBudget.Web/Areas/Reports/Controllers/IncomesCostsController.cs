using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBudget.Web.Areas.Reports.Models.IncomesCosts;
using PersonalBudget.Web.Areas.Reports.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

namespace PersonalBudget.Web.Areas.Reports.Controllers
{
    [Area("Reports")]
    [Authorize]
    public class IncomesCostsController : Controller
    {
        private readonly IIncomesCostsService incomesCostsService;

        public IncomesCostsController(IIncomesCostsService incomesCostsService)
        {
            this.incomesCostsService = incomesCostsService;
        }

       public IActionResult All()
        {
            string userId = this.User.GetId();
            var model = new AllIncomesCostsViewModel
            {
                AllIncomesCosts = this.incomesCostsService.GetAll(userId),
            };
            return this.View(model);
        }
    }
}
