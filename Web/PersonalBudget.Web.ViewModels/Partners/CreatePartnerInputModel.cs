namespace PersonalBudget.Web.ViewModels.Partners
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static PersonalBudget.Data.DataConstants.Partner;

    public class CreatePartnerInputModel
    {
        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        [Display(Name = "Контрагент")]
        public string Name { get; set; }

        [Display(Name = "Град")]
        public int TownId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Towns { get; set; }
    }
}
