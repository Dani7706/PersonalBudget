namespace PersonalBudget.Web.ViewModels.ShoppingCarts
{
    using System.ComponentModel.DataAnnotations;

    using static PersonalBudget.Data.DataConstants.ShoppingCart;

    public class NewShoppingCart
    {
        [Required(ErrorMessage = "Въведете име на количка")]
        [Display(Name = "Списък")]
        [StringLength(MaxCartNameLength, ErrorMessage = "Градът трябва да е между {2} и {1} символа", MinimumLength = MinCartNameLength)]
        public string ShoppingCartName { get; set; }
    }
}
