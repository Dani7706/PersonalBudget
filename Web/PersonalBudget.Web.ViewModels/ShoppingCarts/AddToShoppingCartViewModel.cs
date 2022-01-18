namespace PersonalBudget.Web.ViewModels.ShoppingCarts
{
    using System.ComponentModel.DataAnnotations;

    using static PersonalBudget.Data.DataConstants.ShoppingCart;

    public class AddToShoppingCartViewModel : ProductListViewModel
    {
        [Required(ErrorMessage = "Въведете име на количка")]
        [Display(Name = "Списък")]
        [StringLength(MaxCartNameLength, ErrorMessage = "Градът трябва да е между {2} и {1} символа", MinimumLength = MinCartNameLength)]
        public string ShoppingCartName { get; set; }

        [Display(Name = "Общо")]
        public decimal? Total { get; set; }

    }
}
