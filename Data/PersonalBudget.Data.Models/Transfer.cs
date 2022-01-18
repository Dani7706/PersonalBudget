namespace PersonalBudget.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.Transfer;

    using PersonalBudget.Data.Common;

    public class Transfer : BaseDeletableModel<int>
    {
        public Transfer()
        {
            this.Records = new HashSet<Record>();
            this.Payments = new HashSet<Payment>();
        }

        [Required]
        [DateRangeAttribute]
        public DateTime TransferDate { get; set; }

        public int? PartnerId { get; set; }

        public virtual Partner Partner { get; set; }

        public TransferType TransferType { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<Record> Records { get; set; }

        [Required]
        [Range(MinCapitalValue, MaxCapitalValue)]
        public decimal TotalMoney { get; set; }

        [Required]
        public string UserId { get; set; }

        [Range(MinCapitalValue, MaxCapitalValue)]
        public decimal PaidSum { get; set; }

        [Required]
        [Range(MinCapitalValue, MaxCapitalValue)]
        public decimal RemainingAmount { get; set; }
    }
}
