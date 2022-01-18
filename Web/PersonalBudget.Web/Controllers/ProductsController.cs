namespace PersonalBudget.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.ViewModels.Products;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ISubcategoryService subcategoryService;
        private readonly IMeasureUnitsService measureUnitsService;
        private readonly IMemberService memberService;
        private readonly IDeletableEntityRepository<Item> itemRepository;

        public ProductsController(
            IProductService productService,
            ISubcategoryService subcategoryService,
            IMeasureUnitsService measureUnitsService,
            IMemberService memberService,
            IDeletableEntityRepository<Item> itemRepository)
        {
            this.productService = productService;
            this.subcategoryService = subcategoryService;
            this.measureUnitsService = measureUnitsService;
            this.memberService = memberService;
            this.itemRepository = itemRepository;
        }

        public IActionResult CreateProduct()
        {
            string userId = this.User.GetId();
            var model = new CreateProductInputModel
            {
                SubCategories = this.subcategoryService.GetAllSubCategories(userId),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductInputModel createProduct)
        {
            string userId = this.User.GetId();
            if (!this.ModelState.IsValid)
            {
                createProduct.SubCategories = this.subcategoryService.GetAllSubCategories(userId);
                return this.View(createProduct);
            }

            if (this.productService.Exist(userId, createProduct.Name, createProduct.SubCategoryId) == true)
            {
                this.ModelState.AddModelError(string.Empty, $"{createProduct.Name} вече съществува.");
                createProduct.SubCategories = this.subcategoryService.GetAllSubCategories(userId);
                return this.View(createProduct);
            }

            await this.productService.CreateProduct(createProduct, userId);
            this.TempData["Message"] = $"{createProduct.Name} бе създаден/а успешно";
            return this.Redirect("/Products/All");
        }

        public IActionResult All([FromQuery] AllItemsViewModel allItems)
        {
            string userId = this.User.GetId();
            int itemsPerPage = 10;
            var queryResult = this.productService.GetAll(userId, itemsPerPage, allItems.SearchTerm, allItems.SubCategoryId, allItems.PageNumber);
            allItems.SubCategories = this.subcategoryService.GetAllSubCategories(userId).ToList();
            allItems.Count = queryResult.TotalItems;
            allItems.Items = queryResult.Items;
            allItems.ItemsPerPage = itemsPerPage;

            return this.View(allItems);
        }

        public IActionResult Edit(int id)
        {
            string userId = this.User.GetId();
            var item = this.productService.GetById<EditViewModel>(userId, id);
            item.SubCategories = this.subcategoryService.GetAllSubCategories(userId);
            return this.View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditViewModel editView)
        {
            string userId = this.User.GetId();
            if (!this.ModelState.IsValid)
            {
                editView.SubCategories = this.subcategoryService.GetAllSubCategories(userId);
                return this.View(editView);
            }

            if (this.productService.Exist(userId, editView.Name, editView.SubCategoryId) == true)
            {
                this.ModelState.AddModelError(string.Empty, $"{editView.Name} вече съществува.");
                editView.SubCategories = this.subcategoryService.GetAllSubCategories(userId);
                return this.View(editView);
            }

            await this.productService.Edit(editView, userId, id);
            this.TempData["Message"] = $"{editView.Name} бе редактиран/а успешно";

            return this.Redirect("/Products/All");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            string userId = this.User.GetId();
            this.productService.Delete(userId, id);
            return this.Redirect("/Products/All");
        }
    }
}
