namespace PersonalBudget.Web.ViewModels.Partners
{
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    using static PersonalBudget.Data.DataConstants.Partner;

    public class PartnerViewModel : IMapFrom<Partner>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        [Display(Name = "Контрагент")]
        public string Name { get; set; }

        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        [Display(Name = "Град")]
        public string TownName { get; set; }
    }
}
