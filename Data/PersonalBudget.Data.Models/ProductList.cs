namespace PersonalBudget.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.ProductList;

    public class ProductList : BaseDeletableModel<int>
    {
        public ProductList()
        {
        }

        [Required]
        [Range(MinNameLength, MaxNameLength)]
        public string ProductName { get; set; }

        public int MeasureUnitId { get; set; }

        public virtual MeasureUnit MeasureUnit { get; set; }

        [Range(MinQuantity, MaxQuantity)]
        public decimal? Quantity { get; set; }

        [Range(MinUnitPrice, MaxUnitPrice)]
        public double? UnitPrice { get; set; }

        [Range(MinItemValue, MaxItemValue)]
        public double? ItemValue { get; set; }

        public int ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
