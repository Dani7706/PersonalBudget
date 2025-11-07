using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBudget.Web.Areas.Reports.Models
{
    public class IncomeCostsViewModel 
    {
        [Display(Name = "Всички разходи")]
        public double TotalCosts { get; set; }

        [Display(Name = "Всички разходи")]
        public double TotalIncomes { get; set; }

        public int Month { get; set; }

        public double Difference { get; set; }

        public List<CostViewModel> AllCostsByMember { get; set; }

        public List<CostViewModel> AllIncomesByMember { get; set; }

        public double NikiToPay { get; set; }
    }
}
