namespace PersonalBudget.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.SubCategory;

    public class SubCategory : BaseDeletableModel<int>
    {
        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int? RecordId { get; set; }

        public virtual Record Record { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
