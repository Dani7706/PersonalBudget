namespace PersonalBudget.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.Payment;

    public class Payment : BaseDeletableModel<int>
    {
        public int FinanceTypeId { get; set; }

        public FinanceType FinanceType { get; set; }

        public int FinanceInstitutionId { get; set; }

        public FinanceInstitution FinanceInstitution { get; set; }

        [Required]
        [Range(MinCapitalValue, MaxCapitalValue)]
        public decimal PaidSum { get; set; }

        [Required]
        [Range(MinCapitalValue, MaxCapitalValue)]
        public decimal StartCapital { get; set; }

        [Required]
        [Range(MinCapitalValue, MaxCapitalValue)]
        public decimal FinalCapital { get; set; }

        public int TransferId { get; set; }

        public Transfer Transfer { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
