namespace PersonalBudget.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.ViewModels.Partners;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Authorize]
    public class PartnersController : Controller
    {
        private readonly IPartnerService partnerService;
        private readonly ITownsService townsService;

        public PartnersController(IPartnerService partnerService, ITownsService townsService)
        {
            this.partnerService = partnerService;
            this.townsService = townsService;
        }

        // GET: PartnersController
        public IActionResult CreatePartner()
        {
            string userId = this.User.GetId();
            var model = new CreatePartnerInputModel
            {
                Towns = this.townsService.GetAllTowns(userId),
            };
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePartner(CreatePartnerInputModel model)
        {
            string userId = this.User.GetId();
            if (!this.ModelState.IsValid)
            {
                model.Towns = this.townsService.GetAllTowns(userId);
                return this.View(model);
            }

            if (this.partnerService.Exist(userId, model.Name, model.TownId) == true)
            {
                this.ModelState.AddModelError(string.Empty, $"{model.Name} вече съществува.");
                model.Towns = this.townsService.GetAllTowns(userId);
                return this.View(model);
            }

            await this.partnerService.CreatePartner(model, userId);
            this.TempData["Message"] = $"{model.Name} бе добавен успешно.";

            return this.Redirect("/Partners/All");
        }

        public IActionResult All([FromQuery] AllPartnersViewModel allPartners)
        {
            string userId = this.User.GetId();
            int itemsPerPage = allPartners.ItemsPerPage;
            if (itemsPerPage == 0)
            {
                itemsPerPage = 10;
            }

            var model = new AllPartnersViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = allPartners.PageNumber,
                Count = this.partnerService.GetCount(userId, allPartners.SearchTerm, allPartners.TownId),
                Partners = this.partnerService.GetAll(userId, allPartners.PageNumber, itemsPerPage, allPartners.SearchTerm, allPartners.TownId),
                Towns = this.townsService.GetAllTowns(userId),
            };
            return this.View(model);
        }

        public ActionResult Edit(int id)
        {
            string userId = this.User.GetId();
            var partner = this.partnerService.GetById<EditViewModel>(userId, id);
            partner.Towns = this.townsService.GetAllTowns(userId);
            if (partner == null)
            {
                return this.BadRequest();
            }

            return this.View(partner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EditViewModel editPartner)
        {
            string userId = this.User.GetId();
            var errors = this.ModelState.Values.SelectMany(v => v.Errors);

            if (!this.ModelState.IsValid)
            {
                editPartner.Towns = this.townsService.GetAllTowns(userId);
                return this.View(editPartner);
            }

            if (this.partnerService.Exist(userId, editPartner.Name, editPartner.TownId) == true)
            {
                this.ModelState.AddModelError(string.Empty, $"{editPartner.Name} вече съществува.");
                editPartner.Towns = this.townsService.GetAllTowns(userId);
                return this.View(editPartner);
            }

            await this.partnerService.Edit(editPartner, userId, id);
            this.TempData["Message"] = $"{editPartner.Name} бе редактиран успешно.";

            return this.Redirect("/Partners/All");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            string userId = this.User.GetId();
            await this.partnerService.Delete(userId, id);
            return this.Redirect("/Partners/All");
        }
    }
}
