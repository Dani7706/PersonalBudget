namespace PersonalBudget.Web.ViewModels.Payments
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllPaymentsViewModel : SearchViewModel
    {

        public IEnumerable<PaymentViewModel> Payments { get; set; }

        [Display(Name = "Общо платена сума")]
        public decimal TotalPaidSum { get; set; }
    }
}
