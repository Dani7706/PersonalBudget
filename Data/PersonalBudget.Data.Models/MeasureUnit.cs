namespace PersonalBudget.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.MeasureUnit;

    public class MeasureUnit : BaseDeletableModel<int>
    {
        public MeasureUnit()
        {
            this.Items = new HashSet<MeasureUnitItem>();
            this.Records = new HashSet<Record>();
            this.ProductLists = new HashSet<ProductList>();
        }

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(MinShortNameLength)]
        [MaxLength(MaxShortNameLength)]
        public string ShortName { get; set; }

        public ICollection<ProductList> ProductLists { get; set; }

        public ICollection<Record> Records { get; set; }

        public ICollection<MeasureUnitItem> Items { get; set; }
    }
}
