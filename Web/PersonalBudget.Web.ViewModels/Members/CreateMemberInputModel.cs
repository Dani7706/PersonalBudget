namespace PersonalBudget.Web.ViewModels.Members
{
    using System.ComponentModel.DataAnnotations;

    using static PersonalBudget.Data.DataConstants.Member;

    public class CreateMemberInputModel
    {
        [Required(ErrorMessage = "Въведете име")]
        [StringLength(MaxNameLength, ErrorMessage = "Градът трябва да е между {2} и {1} символа", MinimumLength = MinNameLength)]
        [Display(Name = "Член на семейство")]
        public string Name { get; set; }
    }
}
