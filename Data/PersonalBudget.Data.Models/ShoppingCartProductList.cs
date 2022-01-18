namespace PersonalBudget.Data.Models
{
    public class ShoppingCartProductList
    {
        public int Id { get; set; }

        public int ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public int ProductListId { get; set; }

        public virtual ProductList ProductList { get; set; }
    }
}
