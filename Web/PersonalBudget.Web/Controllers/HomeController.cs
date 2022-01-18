namespace PersonalBudget.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.Areas.Reports.Services;
    using PersonalBudget.Web.ViewModels;
    using PersonalBudget.Web.ViewModels.Home;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    public class HomeController : BaseController
    {
        private readonly ICostsService costsService;
        private readonly IIncomesService incomesService;
        private readonly IFinanceInstitutionService financeInstitutions;
        private readonly SignInManager<IdentityUser> signInManager;

        public HomeController(
            ICostsService costsService,
            IIncomesService incomesService,
            IFinanceInstitutionService financeInstitutions)
        {
            this.costsService = costsService;
            this.incomesService = incomesService;
            this.financeInstitutions = financeInstitutions;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Home/IndexForSignedIn");
            }

            return this.View();
        }

        public IActionResult IndexForSignedIn()
        {
            string userId = this.User.GetId();
            var homeModel = new HomePageViewModel
            {
                AllCosts = this.costsService.TotalCosts(userId),
                AllIncomes = this.incomesService.TotalIncomes(userId),
                FinanceInstitutions = this.financeInstitutions.GetAll(userId),
            };
            return this.View(homeModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
