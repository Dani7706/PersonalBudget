namespace PersonalBudget.Web.ViewModels.Partners
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditViewModel : PartnerViewModel
    {
        [Display(Name = "Град")]
        public int TownId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Towns { get; set; }
    }
}
