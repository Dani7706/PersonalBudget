namespace PersonalBudget.Data.Models
{
    public class RecordFinanceInstitution
    {
        public int Id { get; set; }

        public int FinanceInstitutionId { get; set; }

        public virtual FinanceInstitution FinanceInstitution { get; set; }

        public int RecordId { get; set; }

        public virtual Record Record { get; set; }
    }
}
