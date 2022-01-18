namespace PersonalBudget.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.Town;

    public class Town : BaseDeletableModel<int>
    {
        public Town()
        {
            this.Partners = new HashSet<Partner>();
        }

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(MinCountryNameLength)]
        [MaxLength(MaxCountryNameLength)]
        public string Country { get; set; }

        public ICollection<Partner> Partners { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
