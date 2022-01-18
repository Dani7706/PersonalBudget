namespace PersonalBudget.Web.ViewModels.Products
{
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    public class ItemViewModel : IMapFrom<Item>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SubCategoryName { get; set; }
    }
}
