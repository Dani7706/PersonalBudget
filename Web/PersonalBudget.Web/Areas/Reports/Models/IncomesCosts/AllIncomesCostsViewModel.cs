using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBudget.Web.Areas.Reports.Models.IncomesCosts
{
    public class AllIncomesCostsViewModel
    {
        public List<IncomeCostsViewModel> AllIncomesCosts { get; set; }

        public int CurrentYear { get; set; }
    }
}
