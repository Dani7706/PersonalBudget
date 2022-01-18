namespace PersonalBudget.Web.Components
{
    using System.Linq;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.Records;

    public class ExpireProductsViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Record> recordRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ExpireProductsViewComponent(
            IDeletableEntityRepository<Record> recordRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.recordRepository = recordRepository;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            if (this.recordRepository.All().Count() == 0)
            {
                return this.Content("Все още нямате продукти!");
            }

            var products = this.recordRepository.AllAsNoTracking()
                 .Where(x => x.ExpireDate != null & x.UserId == this.userManager.GetUserId(this.UserClaimsPrincipal) & x.Quantity > 0)
                 .OrderBy(x => x.ExpireDate)
                 .To<ExpireProductViewModel>()
                 .ToList();

            var viewModel = new ExpireProductComponentViewModel { ExpireProducts = products };
            return this.View(viewModel);
        }
    }
}
