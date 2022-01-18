namespace PersonalBudget.Web.ViewModels.Towns
{
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    using static PersonalBudget.Data.DataConstants.Town;

    public class TownViewModel : IMapFrom<Town>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Въведете име на град")]
        [StringLength(MaxNameLength, ErrorMessage = "Градът трябва да е между {2} и {1} символа", MinimumLength = MinNameLength)]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        [Display(Name = "Град")]
        public string Name { get; set; }

        [StringLength(MaxNameLength, ErrorMessage = "Страната трябва да е между {2} и {1} символа", MinimumLength = MinNameLength)]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        [Display(Name = "Страна")]
        public string Country { get; set; }
    }
}
