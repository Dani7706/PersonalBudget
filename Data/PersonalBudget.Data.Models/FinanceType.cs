namespace PersonalBudget.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.FinanceType;

    public class FinanceType : BaseDeletableModel<int>
    {
        public FinanceType()
        {
            this.FinanceInstitutions = new HashSet<FinanceInstitution>();
            this.Payments = new HashSet<Payment>();
        }

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public ICollection<FinanceInstitution> FinanceInstitutions { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
