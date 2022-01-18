namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PersonalBudget.Web.ViewModels.ShoppingCarts;

    public interface IShoppingCartService
    {
        Task AddToShoppingCart(AddToShoppingCartViewModel shoppingCartViewModel, string id);

        T Details<T>(string id, int cartId);

        int Id(string id, string name);

        List<DetailsShoppingCartViewModel> GetAll(string userId);

        Task UpdateTotal(string userId, int cartId, double itemValue);

        Task Delete(string userId, int cartId);

        Task NewShoppingCart(string shoppingCartName, string userId);

        Task AddProduct(ProductListViewModel productListView, string userId, int shoppingCartId);
    }
}
