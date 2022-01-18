namespace PersonalBudget.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.Categories;

    using static PersonalBudget.Data.Common.Exceptions;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task CreateCategory(CreateCategoryInputModel createCategoryInputModel, string userId)
        {
            var category = new Category
            {
                Name = createCategoryInputModel.Name,
                ItemType = createCategoryInputModel.ItemTypes,
                UserId = userId,
            };
            if (this.categoryRepository.All().Any(x => x.UserId == userId & x.Name.ToLower() == createCategoryInputModel.Name.ToLower()))
            {
                throw new Exception(message: "This category already exists.");
            }
            else
            {
                await this.categoryRepository.AddAsync(category);
                await this.categoryRepository.SaveChangesAsync();
            }
        }

        public async Task Delete(string userId, int id)
        {
            var category = this.categoryRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id).FirstOrDefault();
            if (category == null)
            {
                throw new NullReferenceException(NotFoundExceptionMessage);
            }

            this.categoryRepository.Delete(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task Edit(EditViewModel editCategory, int id, string userId)
        {
            var category = this.categoryRepository.AllAsNoTracking()
               .Where(x => x.UserId == userId & x.Id == id).FirstOrDefault();
            if (category == null)
            {
                throw new NullReferenceException(NotFoundExceptionMessage);
            }

            category.Name = editCategory.Name;
            category.ItemType = editCategory.ItemTypes;
            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public bool Exists(string userId, string name, ItemType type)
        {
            var category = this.categoryRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Name == name & x.ItemType == type).FirstOrDefault();
            if (category == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<CategoryViewModel> GetAll(string userId, int currentPage, int itemsPerPage)
        {
            return this.categoryRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Name)
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<CategoryViewModel>()
                .ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllCategories(string userId)
        {
            return this.categoryRepository.All()
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public T GetById<T>(string userId, int id)
        {
            var category = this.categoryRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id)
                .To<T>()
                .FirstOrDefault();
            return category;
        }

        public int GetCount(string userId)
        {
            return this.categoryRepository.AllAsNoTracking().Where(x => x.UserId == userId).Count();
        }
    }
}
