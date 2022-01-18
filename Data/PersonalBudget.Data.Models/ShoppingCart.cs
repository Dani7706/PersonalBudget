namespace PersonalBudget.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.ShoppingCart;

    public class ShoppingCart : BaseDeletableModel<int>
    {
        public ShoppingCart()
        {
            this.ProductLists = new HashSet<ProductList>();
        }

        [Range(MinCartNameLength, MaxCartNameLength)]
        public string Name { get; set; }

        public decimal? Total { get; set; }

        public ICollection<ProductList> ProductLists { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
