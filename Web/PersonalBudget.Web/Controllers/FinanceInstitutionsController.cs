namespace PersonalBudget.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.ViewModels.FinanceInstitutions;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Authorize]
    public class FinanceInstitutionsController : Controller
    {
        private readonly IFinanceInstitutionService financeInstitution;
        private readonly IFinanceTypeService financeTypeService;

        public FinanceInstitutionsController(IFinanceInstitutionService financeInstitution, IFinanceTypeService financeTypeService)
        {
            this.financeInstitution = financeInstitution;
            this.financeTypeService = financeTypeService;
        }

        public IActionResult Create()
        {
            var model = new CreateFinanceInstitutionInputModel
            {
                FinanceTypes = this.financeTypeService.GetAllFinanceTypes(),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFinanceInstitutionInputModel model)
        {
            var errors = this.ModelState.Values.SelectMany(v => v.Errors);

            if (!this.ModelState.IsValid)
            {
                model.FinanceTypes = this.financeTypeService.GetAllFinanceTypes();
                return this.View(model);
            }

            string userId = this.User.GetId();
            if (this.financeInstitution.Exists(userId, model.Name, model.FinanceTypeId) == true)
            {
                this.ModelState.AddModelError(string.Empty, "Институцията вече съществува");
                model.FinanceTypes = this.financeTypeService.GetAllFinanceTypes();

                return this.View(model);
            }

            await this.financeInstitution.CreateFinanceInstitutionsAsync(model, userId);
            this.TempData["Message"] = $"{model.Name} бе добавена успешно.";
            return this.Redirect("/FinanceInstitutions/AllFI");
        }

        public IActionResult AllFI()
        {
            string userId = this.User.GetId();
            var viewModel = new AllFinanceInstitutionsViewModel
            {
                FinanceInstitutions = this.financeInstitution.GetAll(userId),
            };
            return this.View(viewModel);
        }

        public IActionResult GetAllFinanceInstitutionsAsJSON(int financeTypeId)
        {
            string userId = this.User.GetId();
            var financeInstitutions = this.financeInstitution.GetAllFinanceInstitutions(userId, financeTypeId).Select(x => new { Id = x.Id, Name = x.Name }).ToList();

            return this.Json(financeInstitutions);
        }

        public JsonResult GetFinanceInstitutionStartCapitalAsJSON(int financeInstitutionId)
        {
            string userId = this.User.GetId();
            var financeInstitution = this.financeInstitution.GetFinanceInstitutionById<FinanceInstitutionViewModel>(userId, financeInstitutionId);
            var financeCapital = financeInstitution.Capital.ToString();

            return this.Json(new { capital = financeCapital });
        }

        public IActionResult Edit(int id)
        {
            string userId = this.User.GetId();
            var financeInstitution = this.financeInstitution.GetFinanceInstitutionById<EditViewModel>(userId, id);
            financeInstitution.FinanceTypes = this.financeTypeService.GetAllFinanceTypes();
            if (financeInstitution == null)
            {
                return this.NotFound();
            }

            return this.View(financeInstitution);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditViewModel model)
        {
            var errors = this.ModelState.Values.SelectMany(v => v.Errors);

            if (!this.ModelState.IsValid)
            {
                model.FinanceTypes = this.financeTypeService.GetAllFinanceTypes();
                return this.View(model);
            }

            string userId = this.User.GetId();
            if (this.financeInstitution.Exists(userId, model.Name, model.FinanceTypeId) == true)
            {
                this.ModelState.AddModelError(string.Empty, "Институцията вече съществува");
                model.FinanceTypes = this.financeTypeService.GetAllFinanceTypes();
                return this.View(model);
            }

            await this.financeInstitution.Edit(userId, id, model);
            this.TempData["Message"] = $"{model.Name} бе редактирана успешно.";

            return this.Redirect("/FinanceInstitutions/AllFI");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = this.User.GetId();
            await this.financeInstitution.Delete(userId, id);

            return this.Redirect("/FinanceInstitutions/AllFI");
        }
    }
}
