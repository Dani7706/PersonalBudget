namespace PersonalBudget.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.ViewModels.Categories;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult CreateCategory()
        {
            var model = new CreateCategoryInputModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryInputModel categoryInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(categoryInputModel);
            }

            string userId = this.User.GetId();
            if (this.categoryService.Exists(userId, categoryInputModel.Name, categoryInputModel.ItemTypes) == true)
            {
                this.ModelState.AddModelError(string.Empty, "Категорията вече съществува");
                return this.View(categoryInputModel);
            }

            await this.categoryService.CreateCategory(categoryInputModel, userId);
            this.TempData["Message"] = $"{categoryInputModel.Name} бе добавена успешно.";

            return this.Redirect("/Categories/All");
        }

        public IActionResult All([FromQuery] AllCategoriesViewModel allCategories)
        {
            string userId = this.User.GetId();

            int itemsPerPage = allCategories.ItemsPerPage;
            if (itemsPerPage == 0)
            {
                itemsPerPage = 10;
            }

            var viewModel = new AllCategoriesViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = allCategories.PageNumber,
                Count = this.categoryService.GetCount(userId),
                Categories = this.categoryService.GetAll(userId, allCategories.PageNumber, itemsPerPage),
            };
            return this.View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            string userId = this.User.GetId();
            var category = this.categoryService.GetById<EditViewModel>(userId, id);
            if (category == null)
            {
                return this.BadRequest();
            }

            return this.View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditViewModel editCategory)
        {
            string userId = this.User.GetId();
            if (!this.ModelState.IsValid)
            {
                return this.View(editCategory);
            }

            if (this.categoryService.Exists(userId, editCategory.Name, editCategory.ItemTypes) == true)
            {
                this.ModelState.AddModelError(string.Empty, "Категорията вече съществува");
                return this.View(editCategory);
            }

            await this.categoryService.Edit(editCategory, id, userId);
            this.TempData["Message"] = $"{editCategory.Name} бе редактирана успешно.";

            return this.Redirect("/Categories/All");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = this.User.GetId();
            await this.categoryService.Delete(userId, id);
            return this.Redirect("/Categories/All");
        }
    }
}
