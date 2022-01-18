namespace PersonalBudget.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.ViewModels.Towns;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Authorize]
    public class TownsController : Controller
    {
        private readonly ITownsService townService;

        public TownsController(ITownsService townService)
        {
            this.townService = townService;
        }

        public IActionResult CreateTown()
        {
            var model = new CreateTownInputModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTown(CreateTownInputModel createTown)
        {
            string userId = this.User.GetId();
            if (!this.ModelState.IsValid)
            {
                return this.View(createTown);
            }

            if (this.townService.Exists(userId, createTown.Name, createTown.Country) == true)
            {
                this.ModelState.AddModelError(string.Empty, "Градът вече съществува");
                return this.View(createTown);
            }

            await this.townService.Create(createTown, userId);

            this.TempData["Message"] = "Градът бе добавен успешно.";

            return this.Redirect("/Towns/AllTowns");
        }

        public IActionResult All([FromQuery] AllTownsViewModel allTowns)
        {
            string userId = this.User.GetId();
            int itemPerPage = 10;
            var viewModel = new AllTownsViewModel
            {
                Count = this.townService.Count(userId),
                ItemsPerPage = itemPerPage,
                PageNumber = allTowns.PageNumber,
                Towns = this.townService.GetAll(userId, allTowns.PageNumber, itemPerPage),
            };
            return this.View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            string userId = this.User.GetId();
            var town = this.townService.GetById<TownViewModel>(userId, id);
            if (town == null)
            {
                return this.BadRequest();
            }

            return this.View(town);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TownViewModel editTown, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editTown);
            }

            string userId = this.User.GetId();
            if (this.townService.Exists(userId, editTown.Name, editTown.Country) == true)
            {
                this.ModelState.AddModelError(string.Empty, "Градът вече съществува");
                return this.View(editTown);
            }

            await this.townService.Edit(editTown, userId, id);
            this.TempData["Message"] = "Градът бе редактиран успешно.";
            return this.Redirect("/Towns/AllTowns");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = this.User.GetId();
            await this.townService.Delete(userId, id);
            return this.Redirect("/Towns/AllTowns");
        }
    }
}
