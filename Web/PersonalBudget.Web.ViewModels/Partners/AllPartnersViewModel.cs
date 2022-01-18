namespace PersonalBudget.Web.ViewModels.Partners
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllPartnersViewModel : SearchViewModel
    {
        [Display(Name = "Град")]
        public int TownId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Towns { get; set; }

        public IEnumerable<PartnerViewModel> Partners { get; set; }
    }
}
