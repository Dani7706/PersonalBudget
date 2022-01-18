namespace PersonalBudget.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.Category;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.SubCategories = new HashSet<SubCategory>();
        }

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }

        public ItemType ItemType { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
