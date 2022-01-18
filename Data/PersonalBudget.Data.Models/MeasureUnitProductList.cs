namespace PersonalBudget.Data.Models
{
    public class MeasureUnitProductList
    {
        public int Id { get; set; }

        public int MeasureUnitId { get; set; }

        public virtual MeasureUnit MeasureUnit { get; set; }

        public int ProductListId { get; set; }

        public virtual ProductList ProductList { get; set; }

    }
}