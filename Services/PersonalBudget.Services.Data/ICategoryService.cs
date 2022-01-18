namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Web.ViewModels.Categories;

    public interface ICategoryService
    {
        Task CreateCategory(CreateCategoryInputModel createCategoryInputModel, string id);

        IEnumerable<KeyValuePair<string, string>> GetAllCategories(string userId);

        IEnumerable<CategoryViewModel> GetAll(string userId, int currentPage, int itemsPerPage);

        T GetById<T>(string userId, int id);

        Task Edit(EditViewModel editCategory, int id, string userId);

        Task Delete(string userId, int id);

        bool Exists(string userId, string name, ItemType type);

        int GetCount(string userId);
    }
}
