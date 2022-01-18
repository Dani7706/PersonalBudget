namespace PersonalBudget.Data.Models
{
    public class PaymentFinanceType
    {
        public int Id { get; set; }

        public int FinanceTypeId { get; set; }

        public virtual FinanceType FinanceType { get; set; }

        public int PaymentId { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
