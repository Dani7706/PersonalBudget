namespace PersonalBudget.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.Partner;

    public class Partner : BaseDeletableModel<int>
    {
        public Partner()
        {
            this.Transfers = new HashSet<Transfer>();
        }

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        public ICollection<Transfer> Transfers { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
