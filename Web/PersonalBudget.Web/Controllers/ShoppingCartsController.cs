namespace PersonalBudget.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.ViewModels.ShoppingCarts;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Authorize]
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IInventoryService inventoryService;
        private readonly IMeasureUnitsService measureUnitsService;

        public ShoppingCartsController(
            IShoppingCartService shoppingCartService,
            IInventoryService inventoryService,
            IMeasureUnitsService measureUnitsService)
        {
            this.shoppingCartService = shoppingCartService;
            this.inventoryService = inventoryService;
            this.measureUnitsService = measureUnitsService;
        }

        public IActionResult NewShoppingCart()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> NewShoppingCart(NewShoppingCart shoppingCart)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(shoppingCart);
            }

            string userId = this.User.GetId();

            await this.shoppingCartService.NewShoppingCart(shoppingCart.ShoppingCartName, userId);
            this.TempData["Message"] = $"{shoppingCart.ShoppingCartName} бе добавен успешно.";
            int cartId = this.shoppingCartService.Id(userId, shoppingCart.ShoppingCartName);
            return this.RedirectToAction("Details", "ShoppingCarts", new { id = cartId });
        }

        public IActionResult AddProduct(int id)
        {
            var model = new ProductListViewModel();
            model.MeasureUnits = this.measureUnitsService.GetAllMeasureUnits();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductListViewModel productListView, int id)
        {
            if (!this.ModelState.IsValid)
            {
                productListView.MeasureUnits = this.measureUnitsService.GetAllMeasureUnits();
                return this.View(productListView);
            }
            string userId = this.User.GetId();
            await this.shoppingCartService.AddProduct(productListView, userId, id);
            this.TempData["Message"] = $"{productListView.ProductName} бе добавен успешно.";
            return this.RedirectToAction("Details", "ShoppingCarts", new { id = id });
        }

        public IActionResult CreateShoppingCart(int id)
        {
            var model = new AddToShoppingCartViewModel();
            string userId = this.User.GetId();
            model.MeasureUnits = this.measureUnitsService.GetAllMeasureUnits();
            model.ProductName = this.inventoryService.ProductName(userId, id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShoppingCart(AddToShoppingCartViewModel cartViewModel, int id)
        {
            string userId = this.User.GetId();

            if (!this.ModelState.IsValid)
            {
                cartViewModel.MeasureUnits = this.measureUnitsService.GetAllMeasureUnits();
                cartViewModel.ProductName = this.inventoryService.ProductName(userId, id);
                return this.View(cartViewModel);
            }


            await this.shoppingCartService.AddToShoppingCart(cartViewModel, userId);
            this.TempData["Message"] = $"{cartViewModel.ProductName} бе добавен успешно.";
            int cartId = this.shoppingCartService.Id(userId, cartViewModel.ShoppingCartName);
            return this.RedirectToAction("Details", "ShoppingCarts", new { id = cartId });
        }

        public IActionResult Details(int id)
        {
            string userId = this.User.GetId();
            var shoppingCart = this.shoppingCartService.Details<DetailsShoppingCartViewModel>(userId, id);
            if (shoppingCart == null)
            {
                return this.BadRequest();
            }

            return this.View(shoppingCart);
        }

        public IActionResult All()
        {
            string userId = this.User.GetId();
            var cart = new AllShoppingCartsViewModel()
            {
                DetailsShoppingCarts = this.shoppingCartService.GetAll(userId),
            };
            return this.View(cart);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int cartId)
        {
            string userId = this.User.GetId();
            await this.shoppingCartService.Delete(userId, cartId);
            return this.RedirectToAction("All", "ShoppingCarts");
        }
    }
}
