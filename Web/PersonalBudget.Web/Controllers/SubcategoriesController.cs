namespace PersonalBudget.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.ViewModels.Subcategories;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Authorize]
    public class SubcategoriesController : Controller
    {
        private readonly ISubcategoryService subcategoryService;
        private readonly ICategoryService categoryService;

        public SubcategoriesController(ISubcategoryService subcategoryService, ICategoryService categoryService)
        {
            this.subcategoryService = subcategoryService;
            this.categoryService = categoryService;
        }

        public IActionResult CreateSubcategory()
        {
            string userId = this.User.GetId();
            var model = new CreateSubcategoryInputModel
            {
                Categories = this.categoryService.GetAllCategories(userId),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubcategory(CreateSubcategoryInputModel subcategoryInputModel)
        {
            string userId = this.User.GetId();
            if (!this.ModelState.IsValid)
            {
                subcategoryInputModel.Categories = this.categoryService.GetAllCategories(userId);
                return this.View(subcategoryInputModel);
            }

            if (this.subcategoryService.Exists(userId, subcategoryInputModel.Name, subcategoryInputModel.CategoryId) == true)
            {
                this.ModelState.AddModelError(string.Empty, "Подкатегорията вече съществува.");
                subcategoryInputModel.Categories = this.categoryService.GetAllCategories(userId);
                return this.View(subcategoryInputModel);
            }

            await this.subcategoryService.CreateSubcategory(subcategoryInputModel, userId);
            this.TempData["Message"] = "Подкатегорията бе създадена успешно";
            return this.Redirect("/Subcategories/All");
        }

        public List<KeyValuePair<string, string>> GetAllSubCategories()
        {
            string userId = this.User.GetId();
            var subCategories = this.subcategoryService.GetAllSubCategories(userId);
            return subCategories.ToList();
        }

        public IActionResult GetAllSubCategoriesAsJSON()
        {
            string userId = this.User.GetId();
            var subCategories = this.subcategoryService.GetAllSubCategories(userId);
            return this.Json(subCategories);
        }

        public IActionResult All([FromQuery] AllSubcategoriesViewModel viewModel)
        {
            string userId = this.User.GetId();
            int itemsPerPage = viewModel.ItemsPerPage;
            if (itemsPerPage == 0)
            {
                itemsPerPage = 10;
            }

            var model = new AllSubcategoriesViewModel
            {
                ItemsPerPage = itemsPerPage,
                Count = this.subcategoryService.GetCount(userId, viewModel.SearchTerm, viewModel.CategoryId),
                PageNumber = viewModel.PageNumber,
                AllSubcategories = this.subcategoryService.GetAll(userId, viewModel.PageNumber, itemsPerPage, viewModel.SearchTerm, viewModel.CategoryId),
                Categories = this.categoryService.GetAllCategories(userId),
            };
            return this.View(model);
        }

        public IActionResult Edit(int id)
        {
            string userId = this.User.GetId();
            var subCategory = this.subcategoryService.GetById<EditViewModel>(userId, id);
            subCategory.Categories = this.categoryService.GetAllCategories(userId);
            if (subCategory == null)
            {
                return this.BadRequest();
            }

            return this.View(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel editView, int id)
        {
            string userId = this.User.GetId();
            var errors = this.ModelState.Values.SelectMany(v => v.Errors);
            if (!this.ModelState.IsValid)
            {
                editView.Categories = this.categoryService.GetAllCategories(userId);
                return this.View(editView);
            }

            if (this.subcategoryService.Exists(userId, editView.Name, editView.CategoryId) == true)
            {
                this.ModelState.AddModelError(string.Empty, "Подкатегорията вече съществува.");
                editView.Categories = this.categoryService.GetAllCategories(userId);
                return this.View(editView);

            }

            await this.subcategoryService.Edit(editView, userId, id);
            this.TempData["Message"] = "Подкатегорията бе редактирана успешно";
            return this.Redirect("/Subcategories/All");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = this.User.GetId();
            await this.subcategoryService.Delete(userId, id);
            return this.Redirect("/Subcategories/All");
        }
    }
}
