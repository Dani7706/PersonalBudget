namespace PersonalBudget.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum ItemType
    {
        [Display(Name = "Продукт")]
        Product = 1,

        [Display(Name = "Услуга")]
        Service = 2,

        [Display(Name = "Друго")]
        Other = 3,
    }
}
