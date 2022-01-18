namespace PersonalBudget.Web.ViewModels.Members
{
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    using static PersonalBudget.Data.DataConstants.Member;

    public class MemberViewModel : IMapFrom<Member>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        [Display(Name = "Член на семейство")]
        public string Name { get; set; }
    }
}
