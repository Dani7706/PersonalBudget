namespace PersonalBudget.Services.Data
{
    using System.Threading.Tasks;

    using PersonalBudget.Services.Data.Models.Inventory;
    using PersonalBudget.Web.ViewModels.Inventory;

    public interface IInventoryService
    {
        Task AddToInventory(AddToInventoryInputModel productView, string userId);

        InventoryQueryModel InventoryList(string userId, int itemsPerPage, string searchTerm, int subCategoryId = 0, int pageNumber = 1);

        Task Edit(EditViewModel editModel, string userId, int id);

        Task Delete(string userId, int id);

        T GetById<T>(string userId, int id);

        string ProductName(string userId, int id);
    }
}
