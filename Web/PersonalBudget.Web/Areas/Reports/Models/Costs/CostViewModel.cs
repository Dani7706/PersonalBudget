namespace PersonalBudget.Web.Areas.Reports.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CostViewModel
    {
        [Display(Name = "Контрагент")]
        public string PartnerName { get; set; }

        [Display(Name = "Подкатегория")]
        public string SubcategoryName { get; set; }

        [Display(Name = "Потребител")]
        public string MemberName { get; set; }

        [Display(Name = "Стойност")]
        public double ItemValue { get; set; }

        [Display(Name = "Процент спрямо всички разходи")]
        public int CostsPercent { get; set; }

        [Display(Name = "Процент спрямо всички доходи")]
        public int IncomePercent { get; set; }

        [Display(Name = "Дата")]
        public string Date { get; set; }
    }
}
