namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PersonalBudget.Web.ViewModels.Products;

    public interface IProductService
    {
        Task CreateProduct(CreateProductInputModel createProduct, string userId);

        IEnumerable<KeyValuePair<string, string>> GetAllProducts(string userId);

        ItemsQueryModel GetAll(string userId, int itemsPerPage, string searchTerm, int subCategoryId = 0, int pageNumber = 1);

        int GetCount(string userId);

        Task Edit(EditViewModel model, string userId, int id);

        Task Delete(string userId, int id);

        T GetById<T>(string userId, int id);

        bool Exist(string userId, string name, int subcategoryId);
    }
}
