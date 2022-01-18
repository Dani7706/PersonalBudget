namespace PersonalBudget.Web.ViewModels.ShoppingCarts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    public class DetailsShoppingCartViewModel : IMapFrom<ShoppingCart>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Общо")]
        public decimal? Total { get; set; }

        public IEnumerable<ProductListDetailViewModel> ProductLists { get; set; }

    }
}
