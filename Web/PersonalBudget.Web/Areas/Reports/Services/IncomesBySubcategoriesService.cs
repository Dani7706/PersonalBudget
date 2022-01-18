namespace PersonalBudget.Web.Areas.Reports.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Web.Areas.Reports.Models;

    public class IncomesBySubcategoriesService : IIncomesBySubcategoriesService
    {
        private readonly IDeletableEntityRepository<SubCategory> subcategoryRepository;
        private readonly IDeletableEntityRepository<Record> recordRepository;

        public IncomesBySubcategoriesService(
            IDeletableEntityRepository<SubCategory> subcategoryRepository,
            IDeletableEntityRepository<Record> recordRepository)
        {
            this.subcategoryRepository = subcategoryRepository;
            this.recordRepository = recordRepository;
        }


        public List<CostViewModel> GetAllIncomes(string userId, string initialDate, string finalDate, int page, int itemPerPage = 5)
        {
            var incomes = this.recordRepository.AllAsNoTracking()
                .Where(x => x.Transfer.TransferType == TransferType.Income
                & x.UserId == userId);

            if (initialDate != null & finalDate != null)
            {
                incomes = incomes.Where(x => x.Transfer.TransferDate.Date >= Convert.ToDateTime(initialDate)
                 & x.Transfer.TransferDate.Date <= Convert.ToDateTime(finalDate));
            }
            var groupedincomes = incomes.GroupBy(x => x.SubCategoryId)
                 .Select(x => new CostViewModel
                 {
                     SubcategoryName = this.subcategoryRepository.AllAsNoTracking().Where(y => y.Id == x.Key).FirstOrDefault().Name,
                     ItemValue = (double)x.Sum(c => c.ItemValue),
                 })
                 .OrderBy(x => x.SubcategoryName)
                 .ToList();
            return groupedincomes;
        }
    }
}
