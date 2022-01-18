namespace PersonalBudget.Web.ViewModels.Inventory
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    public class InventoryRecordViewModel : IMapFrom<Record>
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public string SubCategoryName { get; set; }

        public string Quantity { get; set; }

        public string MeasureUnitShortName { get; set; }

        public string MemberName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime ExpireDate { get; set; }
    }
}
