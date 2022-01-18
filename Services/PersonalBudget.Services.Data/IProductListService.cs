namespace PersonalBudget.Services.Data
{
    using System.Threading.Tasks;

    public interface IProductListService
    {
        Task Delete(string userId, int id, int cartId);
    }
}
