namespace PersonalBudget.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using global::PersonalBudget.Services.Data;
    using global::PersonalBudget.Web.ViewModels.Records;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Authorize]
    public class RecordsController : Controller
    {
        private readonly IRecordsService recordsService;
        private readonly IProductService productService;
        private readonly IMemberService memberService;
        private readonly ISubcategoryService subcategoryService;
        private readonly IMeasureUnitsService measureUnitsService;

        public RecordsController(
            IRecordsService recordsService,
            IProductService productService,
            IMemberService memberService,
            ISubcategoryService subcategoryService,
            IMeasureUnitsService measureUnitsService)
        {
            this.recordsService = recordsService;
            this.productService = productService;
            this.memberService = memberService;
            this.subcategoryService = subcategoryService;
            this.measureUnitsService = measureUnitsService;
        }

        public IActionResult Edit(int recordId)
        {
            string userId = this.User.GetId();
            var editRecord = this.recordsService.GetById<RecordViewModel>(userId, recordId);
            editRecord.Items = this.productService.GetAllProducts(userId);
            editRecord.Members = this.memberService.GetAllMembers(userId);
            editRecord.Subcategories = this.subcategoryService.GetAllSubCategories(userId);
            editRecord.MeasureUnits = this.measureUnitsService.GetAllMeasureUnits();
            editRecord.TransferId = this.recordsService.GetTransferId(userId, recordId);
            return this.View(editRecord);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int recordId, RecordViewModel editRecord)
        {
            string userId = this.User.GetId();
            int transferId = this.recordsService.GetTransferId(userId, recordId);
            var errors = this.ModelState.Values.SelectMany(v => v.Errors);
            if (!this.ModelState.IsValid)
            {
                editRecord.Items = this.productService.GetAllProducts(userId);
                editRecord.Members = this.memberService.GetAllMembers(userId);
                editRecord.Subcategories = this.subcategoryService.GetAllSubCategories(userId);
                editRecord.MeasureUnits = this.measureUnitsService.GetAllMeasureUnits();

                return this.View(editRecord);
            }

            await this.recordsService.EditRecord(editRecord, userId, recordId);
            this.TempData["Message"] = "Записът бе редактиран успешно.";

            return this.RedirectToAction("Edit", "Transfers", new { id = transferId });
        }

        public IActionResult All(int id = 1)
        {
            string userId = this.User.GetId();
            return this.View();
        }
    }
}
