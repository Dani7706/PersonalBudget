namespace PersonalBudget.Web.ViewModels.Payments
{
    using System;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    public class PaymentViewModel : IMapFrom<Payment>
    {
        public DateTime CreatedOn { get; set; }

        public string FinanceInstitutionName { get; set; }

        public decimal StartCapital { get; set; }

        public decimal PaidSum { get; set; }

        public decimal FinalCapital { get; set; }
    }
}
