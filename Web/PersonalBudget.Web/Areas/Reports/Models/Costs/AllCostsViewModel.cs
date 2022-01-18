namespace PersonalBudget.Web.Areas.Reports.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Web.ViewModels;

    public class AllCostsViewModel : SearchViewModel
    {
        public List<CostViewModel> AllCosts { get; set; }

        [Display(Name = "Всички разходи")]
        public double TotalCosts { get; set; }
    }
}
