namespace PersonalBudget.Web.ViewModels.ShoppingCarts
{
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    public class ProductListDetailViewModel : IMapFrom<ProductList>
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string MeasureUnitShortName { get; set; }

        public decimal Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double ItemValue { get; set; }

        public bool IsDeleted { get; set; }
    }
}
