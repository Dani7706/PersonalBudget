namespace PersonalBudget.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.Item;

    public class Item : BaseDeletableModel<int>
    {
        public Item()
        {
            this.Records = new HashSet<Record>();
        }

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public ICollection<Record> Records { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
