namespace PersonalBudget.Web.ViewModels.Records
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    public class ExpireProductViewModel : IMapFrom<Record>
    {
        public string ItemName { get; set; }

        public string Quantity { get; set; }

        public string MeasureUnitShortName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime ExpireDate { get; set; }
    }
}
