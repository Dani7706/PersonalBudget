namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.Subcategories;

    public class SubcategoryService : ISubcategoryService
    {
        private readonly IDeletableEntityRepository<SubCategory> subcategoryRepository;

        public SubcategoryService(IDeletableEntityRepository<SubCategory> subcategoryRepository)
        {
            this.subcategoryRepository = subcategoryRepository;
        }

        public async Task CreateSubcategory(CreateSubcategoryInputModel createSubcategoryInputModel, string userId)
        {
            var subcategory = new SubCategory
            {
                Name = createSubcategoryInputModel.Name,
                CategoryId = createSubcategoryInputModel.CategoryId,
                UserId = userId,
            };
            await this.subcategoryRepository.AddAsync(subcategory);
            await this.subcategoryRepository.SaveChangesAsync();
        }

        public async Task Delete(string userId, int id)
        {
            var subCategory = this.subcategoryRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id).FirstOrDefault();
            this.subcategoryRepository.Delete(subCategory);
            await this.subcategoryRepository.SaveChangesAsync();
        }

        public async Task Edit(EditViewModel editSubcategory, string userId, int id)
        {
            var subCategory = this.subcategoryRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id).FirstOrDefault();
            subCategory.Name = editSubcategory.Name;
            subCategory.CategoryId = editSubcategory.CategoryId;
            this.subcategoryRepository.Update(subCategory);
            await this.subcategoryRepository.SaveChangesAsync();
        }

        public bool Exists(string userId, string name, int categoryId)
        {
            var subcategory = this.subcategoryRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Name.ToLower() == name.ToLower() & x.CategoryId == categoryId).FirstOrDefault();
            if (subcategory != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<SubcategoryViewModel> GetAll(string userId, int currentPage, int itemsPerPage, string search, int categoryId)
        {
            var subcategoryList = this.SubcategoriesAfterSearch<SubcategoryViewModel>(userId, search, categoryId);

            var subcategories = subcategoryList
                .OrderBy(x => x.Name)
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();
            return subcategories;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllSubCategories(string userId)
        {
            return this.subcategoryRepository.All()
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<KeyValuePair<string, string>> GetProductSubCategories(string userId)
        {
            return this.subcategoryRepository.All()
                .Where(x => x.UserId == userId & x.Category.ItemType == ItemType.Product)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public T GetById<T>(string userId, int id)
        {
            var subCategory = this.subcategoryRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id)
                .To<T>()
                .FirstOrDefault();
            return subCategory;
        }

        public int GetCount(string userId, string search, int categoryId)
        {
            int count = this.SubcategoriesAfterSearch<SubcategoryViewModel>(userId, search, categoryId).Count;

            return count;
        }

        public List<T> SubcategoriesAfterSearch<T>(string userId, string search, int categoryId)
        {
            var subcategoryList = this.subcategoryRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId);
            if (!string.IsNullOrWhiteSpace(search))
            {
                subcategoryList = subcategoryList.Where(x => x.Name.ToLower().Contains(search.ToLower()));
            }

            if (categoryId > 0)
            {
                subcategoryList = subcategoryList.Where(x => x.CategoryId == categoryId);
            }

            return subcategoryList.To<T>().ToList();
        }
    }
}
