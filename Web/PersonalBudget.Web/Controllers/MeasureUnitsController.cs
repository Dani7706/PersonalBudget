namespace PersonalBudget.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.ViewModels.MeasureUnits;

    [Authorize]
    public class MeasureUnitsController : Controller
    {
        private readonly IMeasureUnitsService measureUnitsService;

        public MeasureUnitsController(IMeasureUnitsService measureUnitsService)
        {
            this.measureUnitsService = measureUnitsService;
        }

        public IActionResult CreateMeasureUnit()
        {
            var model = new CreateMeasureUnitInputModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeasureUnit(CreateMeasureUnitInputModel createMeasureUnit)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createMeasureUnit);
            }

            if (this.measureUnitsService.Exists(createMeasureUnit.ShortName, createMeasureUnit.Name) == true)
            {
                this.ModelState.AddModelError(string.Empty, "Мерната единица вече съществува");

                return this.View(createMeasureUnit);
            }

            await this.measureUnitsService.CreateMeasureUnit(createMeasureUnit);
            this.TempData["Message"] = $"{createMeasureUnit.Name} бе добавена успешно.";

            return this.Redirect("/MeasureUnits/AllMeasureUnits");
        }

        public IActionResult AllMeasureUnits()
        {
            var viewModel = new AllMeasureUnits
            {
                MeasureUnits = this.measureUnitsService.GetAll(),
            };
            return this.View(viewModel);
        }
    }
}
