namespace PersonalBudget.Web.ViewModels.ShoppingCarts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static PersonalBudget.Data.DataConstants.ProductList;

    public class ProductListViewModel
    {
        [Display(Name ="Продукт")]
        [Required(ErrorMessage = "Въведете име на продукт")]
        [StringLength(MaxNameLength, ErrorMessage = "Продуктът трябва да е между {2} и {1} символа", MinimumLength = MinNameLength)]
        public string ProductName { get; set; }

        [Display(Name = "Мерна единица")]
        public int MeasureUnitId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> MeasureUnits { get; set; }

        [Display(Name = "Количество")]
        public string Quantity { get; set; }

        [Display(Name = "Ед. цена")]
        public string Price { get; set; }

        [Display(Name = "Стойност")]
        public string ProductValue { get; set; }
    }
}
