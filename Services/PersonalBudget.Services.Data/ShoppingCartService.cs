namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.ShoppingCarts;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDeletableEntityRepository<ShoppingCart> shoppingCartRepository;
        private readonly IDeletableEntityRepository<ProductList> productListRepository;

        public ShoppingCartService(
            IDeletableEntityRepository<ShoppingCart> shoppingCartRepository,
            IDeletableEntityRepository<ProductList> productListRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productListRepository = productListRepository;
        }

        public async Task AddProduct(ProductListViewModel productListView, string userId, int shoppingCartId)
        {
            var shoppingCart = this.shoppingCartRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == shoppingCartId).FirstOrDefault();
            var product = new ProductList()
            {
                ProductName = productListView.ProductName,
                UnitPrice = double.Parse(productListView.Price, CultureInfo.InvariantCulture),
                Quantity = decimal.Parse(productListView.Quantity, CultureInfo.InvariantCulture),
                ItemValue = double.Parse(productListView.ProductValue, CultureInfo.InvariantCulture),
                MeasureUnitId = productListView.MeasureUnitId,
                ShoppingCartId = shoppingCartId,
                UserId = userId,
            };
            await this.productListRepository.AddAsync(product);
            await this.productListRepository.SaveChangesAsync();

            var total = this.productListRepository.AllAsNoTracking()
               .Where(x => x.UserId == userId & x.ShoppingCartId == shoppingCartId)
               .Select(x => x.ItemValue).Sum();
            shoppingCart.Total = decimal.Parse(total.ToString());
            shoppingCart.ProductLists.Add(product);
            this.shoppingCartRepository.Update(shoppingCart);
            await this.shoppingCartRepository.SaveChangesAsync();
        }

        public async Task AddToShoppingCart(AddToShoppingCartViewModel shoppingCartViewModel, string id)
        {
            var shoppingCart = this.shoppingCartRepository.AllAsNoTracking()
                .Where(x => x.UserId == id & x.Name == shoppingCartViewModel.ShoppingCartName).FirstOrDefault();
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    Name = shoppingCartViewModel.ShoppingCartName,
                    UserId = id,
                };
                await this.shoppingCartRepository.AddAsync(shoppingCart);
                await this.shoppingCartRepository.SaveChangesAsync();
            }
            int cartId = this.Id(id, shoppingCartViewModel.ShoppingCartName);
            var product = new ProductList()
            {
                ProductName = shoppingCartViewModel.ProductName,
                UnitPrice = double.Parse(shoppingCartViewModel.Price, CultureInfo.InvariantCulture),
                Quantity = decimal.Parse(shoppingCartViewModel.Quantity, CultureInfo.InvariantCulture),
                ItemValue = double.Parse(shoppingCartViewModel.ProductValue, CultureInfo.InvariantCulture),
                MeasureUnitId = shoppingCartViewModel.MeasureUnitId,
                ShoppingCartId = cartId,
                UserId = id,
            };
            await this.productListRepository.AddAsync(product);
            await this.productListRepository.SaveChangesAsync();

            var total = this.productListRepository.AllAsNoTracking()
               .Where(x => x.UserId == id & x.ShoppingCart.Name == shoppingCartViewModel.ShoppingCartName)
               .Select(x => x.ItemValue).Sum();
            shoppingCart.Total = decimal.Parse(total.ToString());
            shoppingCart.ProductLists.Add(product);
            this.shoppingCartRepository.Update(shoppingCart);
            await this.shoppingCartRepository.SaveChangesAsync();

        }

        public async Task Delete(string userId, int cartId)
        {
            var cart = this.shoppingCartRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == cartId).FirstOrDefault();
            this.shoppingCartRepository.Delete(cart);
            await this.shoppingCartRepository.SaveChangesAsync();
        }

        public T Details<T>(string id, int cartId)
        {
            var shoppingCart = this.shoppingCartRepository.AllAsNoTracking()
                .Where(x => x.UserId == id & x.Id == cartId)
                .To<T>()
                .FirstOrDefault();
            return shoppingCart;
        }

        public List<DetailsShoppingCartViewModel> GetAll(string userId)
        {
            var carts = this.shoppingCartRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .To<DetailsShoppingCartViewModel>()
                .ToList();
            return carts;
        }

        public int Id(string id, string name)
        {
            int cartId = this.shoppingCartRepository.AllAsNoTracking()
                .Where(x => x.UserId == id & x.Name == name).FirstOrDefault().Id;
            return cartId;
        }

        public async Task NewShoppingCart(string newShoppingCart, string userId)
        {
            var shoppingCart = this.shoppingCartRepository.AllAsNoTracking()
                 .Where(x => x.UserId == userId & x.Name == newShoppingCart).FirstOrDefault();
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    Name = newShoppingCart,
                    UserId = userId,
                };
                await this.shoppingCartRepository.AddAsync(shoppingCart);
                await this.shoppingCartRepository.SaveChangesAsync();
            }

        }

        public async Task UpdateTotal(string userId, int cartId, double itemValue)
        {
            var cart = this.shoppingCartRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == cartId).FirstOrDefault();
            var cartTotal = cart.Total;
            cartTotal -= (decimal)itemValue;
            cart.Total = cartTotal;
            this.shoppingCartRepository.Update(cart);
            await this.shoppingCartRepository.SaveChangesAsync();
        }
    }
}
