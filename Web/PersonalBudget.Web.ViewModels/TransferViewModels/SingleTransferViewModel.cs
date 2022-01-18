namespace PersonalBudget.Web.ViewModels.TransferViewModels
{
    using System.Collections.Generic;

    using PersonalBudget.Web.ViewModels.Payments;

    public class SingleTransferViewModel : TransferViewModel
    {
        public IEnumerable<PaymentViewModel> Payments { get; set; }
    }
}
