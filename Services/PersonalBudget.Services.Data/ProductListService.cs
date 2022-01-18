namespace PersonalBudget.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;

    public class ProductListService : IProductListService
    {
        private readonly IDeletableEntityRepository<ProductList> productListRepository;
        private readonly IDeletableEntityRepository<ShoppingCart> shoppingCartRepository;
        private readonly IShoppingCartService shoppingCartService;

        public ProductListService(
            IDeletableEntityRepository<ProductList> productListRepository,
            IDeletableEntityRepository<ShoppingCart> shoppingCartRepository,
            IShoppingCartService shoppingCartService)
        {
            this.productListRepository = productListRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.shoppingCartService = shoppingCartService;
        }

        public async Task Delete(string userId, int id, int cartId)
        {
            var product = this.productListRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id).FirstOrDefault();
            var itemValue = (double)product.ItemValue;
            this.productListRepository.Delete(product);
            await this.productListRepository.SaveChangesAsync();
            await this.shoppingCartService.UpdateTotal(userId, cartId, itemValue);

        }
    }
}
