namespace PersonalBudget.Web.ViewModels.Records
{
    using System;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    public class DetailsViewModel : IMapFrom<Record>
    {
        public string ItemName { get; set; }

        public string SubCategoryName { get; set; }

        public string Quantity { get; set; }

        public string MeasureUnitName { get; set; }

        public string UnitPrice { get; set; }

        public string MemberName { get; set; }

        public DateTime? ExpireDate { get; set; }

        public string ItemValue { get; set; }
    }
}
