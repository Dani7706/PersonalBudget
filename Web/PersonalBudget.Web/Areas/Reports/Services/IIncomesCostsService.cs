﻿using PersonalBudget.Web.Areas.Reports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBudget.Web.Areas.Reports.Services
{
    public interface IIncomesCostsService
    {
        List<IncomeCostsViewModel> GetAll(string userId);

        List<CostViewModel> AllCostsByMember(string userId, int month);

        List<CostViewModel> AllIncomesByMember(string userId, int month);
    }
}
