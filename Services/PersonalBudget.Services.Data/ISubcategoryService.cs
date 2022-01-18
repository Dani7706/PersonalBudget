namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PersonalBudget.Web.ViewModels.Subcategories;

    public interface ISubcategoryService
    {
        Task CreateSubcategory(CreateSubcategoryInputModel createSubcategoryInputModel, string userId);

        IEnumerable<KeyValuePair<string, string>> GetAllSubCategories(string userId);

        IEnumerable<KeyValuePair<string, string>> GetProductSubCategories(string userId);

        IEnumerable<SubcategoryViewModel> GetAll(string userId, int currentPage, int itemsPerPage, string search, int categoryId);

        T GetById<T>(string userId, int id);

        Task Edit(EditViewModel editSubcategory, string userId, int id);

        Task Delete(string userId, int id);

        int GetCount(string userId, string search, int categoryId);

        List<T> SubcategoriesAfterSearch<T>(string userId, string search, int categoryId);

        bool Exists(string userId, string name, int categoryId);
    }
}
