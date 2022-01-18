namespace PersonalBudget.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.FinanceInstitution;

    public class FinanceInstitution : BaseDeletableModel<int>
    {
        public FinanceInstitution()
        {
            this.Payments = new HashSet<Payment>();
        }

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public int FinanceTypeId { get; set; }

        public virtual FinanceType FinanceType { get; set; }

        [Required]
        [Range(MinCapitalValue, MaxCapitalValue)]
        public decimal Capital { get; set; }

        public ICollection<Payment> Payments { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
