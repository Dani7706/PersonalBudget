namespace PersonalBudget.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common.Models;

    using static PersonalBudget.Data.DataConstants.Record;

    public class Record : BaseDeletableModel<int>
    {
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        [Required]
        [Range(MinQuantity, MaxQuantity)]
        public double Quantity { get; set; }

        public int MeasureUnitId { get; set; }

        public virtual MeasureUnit MeasureUnit { get; set; }

        [Range(MinUnitPrice, MaxUnitPrice)]
        public double? UnitPrice { get; set; }

        public int MemberId { get; set; }

        public virtual Member Member { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), MinDate, MaxDate, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? ExpireDate { get; set; }

        public int? TransferId { get; set; }

        public virtual Transfer Transfer { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        [Range(MinItemValue, MaxItemValue)]
        public double? ItemValue { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
