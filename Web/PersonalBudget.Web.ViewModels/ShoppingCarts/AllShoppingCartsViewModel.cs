namespace PersonalBudget.Web.ViewModels.ShoppingCarts
{
    using System.Collections.Generic;

    public class AllShoppingCartsViewModel
    {
        public IEnumerable<DetailsShoppingCartViewModel> DetailsShoppingCarts { get; set; }
    }
}
