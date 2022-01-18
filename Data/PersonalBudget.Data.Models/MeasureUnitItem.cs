namespace PersonalBudget.Data.Models
{
    public class MeasureUnitItem
    {
        public int Id { get; set; }

        public int MeasureUnitId { get; set; }

        public virtual MeasureUnit MeasureUnit { get; set; }

        public int ItemId { get; set; }

        public virtual Record Item { get; set; }
    }
}
