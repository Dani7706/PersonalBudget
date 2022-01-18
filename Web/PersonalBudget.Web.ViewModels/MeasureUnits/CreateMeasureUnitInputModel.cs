namespace PersonalBudget.Web.ViewModels.MeasureUnits
{
    using System.ComponentModel.DataAnnotations;

    using static PersonalBudget.Data.DataConstants.MeasureUnit;

    public class CreateMeasureUnitInputModel
    {
        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        [Display(Name = "Мерна единица")]
        public string Name { get; set; }

        [Required]
        [MinLength(MinShortNameLength)]
        [MaxLength(MaxShortNameLength)]
        [Display(Name = "Кратко наименование")]
        public string ShortName { get; set; }
    }
}
