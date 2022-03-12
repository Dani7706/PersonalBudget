using PersonalBudget.Data.Common.Repositories;
using PersonalBudget.Data.Models;
using PersonalBudget.Web.Areas.Reports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBudget.Web.Areas.Reports.Services
{
    public class IncomesCostsService : IIncomesCostsService
    {
        private readonly IDeletableEntityRepository<Transfer> transferRepository;
        private readonly IDeletableEntityRepository<Record> recordRepository;
        private readonly IDeletableEntityRepository<Member> memberRepository;

        public IncomesCostsService(IDeletableEntityRepository<Transfer> transferRepository,
            IDeletableEntityRepository<Record>recordRepository,
            IDeletableEntityRepository<Member>memberRepository)
        {
            this.transferRepository = transferRepository;
            this.recordRepository = recordRepository;
            this.memberRepository = memberRepository;
        }

        public List<CostViewModel> AllCostsByMember(string userId, int month)
        {
            var costs = this.recordRepository.AllAsNoTracking()
               .Where(x => x.Transfer.TransferType == TransferType.Payment & x.Transfer.TransferDate.Month == month
               & x.UserId == userId);

            

            var groupedcosts = costs.GroupBy(x => x.MemberId)
                 .Select(x => new CostViewModel
                 {
                     MemberName = this.memberRepository.AllAsNoTracking().Where(y => y.Id == x.Key).FirstOrDefault().Name,
                     ItemValue =Math.Round((double)x.Sum(c => c.ItemValue),2),
                 })
                 .ToList();
            return groupedcosts;
        }

        public List<CostViewModel> AllIncomesByMember(string userId, int month)
        {
            var incomes = this.recordRepository.AllAsNoTracking()
               .Where(x => x.Transfer.TransferType == TransferType.Income & x.Transfer.TransferDate.Month==month
               & x.UserId == userId);



            var groupedcosts = incomes.GroupBy(x => x.MemberId)
                 .Select(x => new CostViewModel
                 {
                     MemberName = this.memberRepository.AllAsNoTracking().Where(y => y.Id == x.Key).FirstOrDefault().Name,
                     ItemValue =Math.Round((double)x.Sum(c => c.ItemValue),2),
                 })
                 .ToList();
            return groupedcosts;
        }

        public List<IncomeCostsViewModel> GetAll(string userId)
        {
            int month = DateTime.Now.Month;
            var incomesCostsModel = new List<IncomeCostsViewModel>();
            for (int i = 1; i <= month; i++)
            {
                var incomeCostModel = new IncomeCostsViewModel();
                var totalIncomeSum = this.transferRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.TransferType == TransferType.Income & x.TransferDate.Month==i)
                .Select(x => x.TotalMoney)
                .Sum();
                var totalCostsSum = this.transferRepository.AllAsNoTracking()
                    .Where(x => x.UserId == userId & x.TransferType == TransferType.Payment & x.TransferDate.Month==i)
                    .Select(x => x.TotalMoney)
                    .Sum();
                incomeCostModel.TotalCosts = Convert.ToDouble(totalCostsSum);
                incomeCostModel.TotalIncomes = Convert.ToDouble(totalIncomeSum);
                incomeCostModel.Month = i;
                incomeCostModel.Difference = Math.Round(incomeCostModel.TotalIncomes - incomeCostModel.TotalCosts, 2);
                incomeCostModel.AllCostsByMember = this.AllCostsByMember(userId, i);
                incomeCostModel.AllIncomesByMember = this.AllIncomesByMember(userId, i);
                incomesCostsModel.Add(incomeCostModel);

            }
            return incomesCostsModel.ToList();
            
        }
    }
}
