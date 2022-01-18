namespace PersonalBudget.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum TransferType
    {
        [Display(Name = "Друг")]
        Other = 0,
        [Display(Name = "Доход")]
        Income = 1,
        [Display(Name = "Разход")]
        Payment = 2,
    }
}
