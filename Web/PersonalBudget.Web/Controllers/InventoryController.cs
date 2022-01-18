namespace PersonalBudget.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.ViewModels.Inventory;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    public class InventoryController : Controller
    {
        private readonly IRecordsService recordsService;
        private readonly IProductService productService;
        private readonly IMemberService memberService;
        private readonly ISubcategoryService subcategoryService;
        private readonly IMeasureUnitsService measureUnitsService;
        private readonly IInventoryService inventoryService;

        public InventoryController(
           IRecordsService recordsService,
           IProductService productService,
           IMemberService memberService,
           ISubcategoryService subcategoryService,
           IMeasureUnitsService measureUnitsService,
           IInventoryService inventoryService)
        {
            this.recordsService = recordsService;
            this.productService = productService;
            this.memberService = memberService;
            this.subcategoryService = subcategoryService;
            this.measureUnitsService = measureUnitsService;
            this.inventoryService = inventoryService;
        }

        public IActionResult Edit(int id)
        {
            string userId = this.User.GetId();
            var editRecord = this.inventoryService.GetById<EditViewModel>(userId, id);
            editRecord.Items = this.productService.GetAllProducts(userId);
            editRecord.Members = this.memberService.GetAllMembers(userId);
            editRecord.Subcategories = this.subcategoryService.GetAllSubCategories(userId);
            editRecord.MeasureUnits = this.measureUnitsService.GetAllMeasureUnits();
            return this.View(editRecord);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditViewModel editRecord)
        {
            string userId = this.User.GetId();
            var errors = this.ModelState.Values.SelectMany(v => v.Errors);
            if (!this.ModelState.IsValid)
            {
                editRecord.Items = this.productService.GetAllProducts(userId);
                editRecord.Members = this.memberService.GetAllMembers(userId);
                editRecord.Subcategories = this.subcategoryService.GetAllSubCategories(userId);
                editRecord.MeasureUnits = this.measureUnitsService.GetAllMeasureUnits();

                return this.View(editRecord);
            }

            await this.inventoryService.Edit(editRecord, userId, id);
            this.TempData["Message"] = $"Продуктът бе успешно редактиран.";

            return this.RedirectToAction("All", "Inventory");
        }

        public IActionResult AddInitialAvailability()
        {
            string userId = this.User.GetId();
            var model = new AddToInventoryInputModel
            {
                Subcategories = this.subcategoryService.GetAllSubCategories(userId),
                Items = this.productService.GetAllProducts(userId),
                MeasureUnits = this.measureUnitsService.GetAllMeasureUnits(),
                Members = this.memberService.GetAllMembers(userId),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddInitialAvailability(AddToInventoryInputModel recordView)
        {
            string userId = this.User.GetId();
            var errors = this.ModelState.Values.SelectMany(v => v.Errors);

            if (!this.ModelState.IsValid)
            {
                recordView.Subcategories = this.subcategoryService.GetAllSubCategories(userId);
                recordView.Items = this.productService.GetAllProducts(userId);
                recordView.MeasureUnits = this.measureUnitsService.GetAllMeasureUnits();
                recordView.Members = this.memberService.GetAllMembers(userId);
                return this.View(recordView);
            }

            await this.inventoryService.AddToInventory(recordView, userId);
            this.TempData["Message"] = "Продуктът бе добавен успешно.";

            return this.Redirect("/Inventory/All");
        }

        public IActionResult All([FromQuery] AllInventoryProductsViewModel allInventory)
        {
            string userId = this.User.GetId();

            int itemsPerPage = 10;

            var queryResult = this.inventoryService.InventoryList(userId, itemsPerPage, allInventory.SearchTerm, allInventory.SubCategoryId, allInventory.PageNumber);
            allInventory.SubCategories = this.subcategoryService.GetProductSubCategories(userId).ToList();
            allInventory.Count = queryResult.TotalProducts;
            allInventory.Products = queryResult.Products;
            allInventory.ItemsPerPage = queryResult.ItemsPerPage;
            return this.View(allInventory);
        }

        public async Task<IActionResult> Update(int id)
        {
            string userId = this.User.GetId();
            await this.inventoryService.Delete(userId, id);
            return this.Redirect("/Inventory/All");
        }
    }
}
