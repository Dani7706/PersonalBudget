namespace PersonalBudget.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    public class ProductListsController : Controller
    {
        private readonly IProductListService productListService;
        private readonly IShoppingCartService shoppingCartService;

        public ProductListsController(
            IProductListService productListService,
            IShoppingCartService shoppingCartService)
        {
            this.productListService = productListService;
            this.shoppingCartService = shoppingCartService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int cartId, int productId, double itemValue)
        {
            string userId = this.User.GetId();
            await this.productListService.Delete(userId, productId, cartId);
            await this.shoppingCartService.UpdateTotal(userId, cartId, itemValue);
            return this.RedirectToAction("Details", "ShoppingCarts", new { id = cartId });
        }
    }
}
