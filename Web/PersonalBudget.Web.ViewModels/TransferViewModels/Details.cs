namespace PersonalBudget.Web.ViewModels.TransferViewModels
{
    using System;
    using System.Collections.Generic;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.Records;

    public class Details : IMapFrom<Transfer>
    {
        public int Id { get; set; }

        public string TransferType { get; set; }

        public string PartnerName { get; set; }

        public DateTime TransferDate { get; set; }

        public string TotalMoney { get; set; }

        public IEnumerable<DetailsViewModel> Records { get; set; }
    }
}
