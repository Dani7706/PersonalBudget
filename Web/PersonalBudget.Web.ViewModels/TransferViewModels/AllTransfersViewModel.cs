namespace PersonalBudget.Web.ViewModels.TransferViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllTransfersViewModel : PagingViewModel
    {
        [Display(Name = "Сортиране")]
        public int PartnerId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Partners { get; set; }

        [Display(Name = "Търсене")]
        public string SearchTerm { get; set; }

        public IEnumerable<TransferViewModel> Transfers { get; set; }
    }
}
