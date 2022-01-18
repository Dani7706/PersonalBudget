namespace PersonalBudget.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ContactFormViewModel
    {
        [Display(Name = "Вашите имена")]
        public string Name { get; set; }

        [Display(Name = "Вашият email адрес")]
        public string Email { get; set; }

        [Display(Name = "Заглавие на съобщението")]
        public string Title { get; set; }

        [Display(Name = "Съдържание на съобщението")]
        public string Content { get; set; }
    }
}
