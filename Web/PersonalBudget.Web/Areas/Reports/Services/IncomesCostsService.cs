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
            IDeletableEntityRepository<Record> recordRepository,
            IDeletableEntityRepository<Member> memberRepository)
        {
            this.transferRepository = transferRepository;
            this.recordRepository = recordRepository;
            this.memberRepository = memberRepository;
        }

        public List<CostViewModel> AllCostsByMember(string userId, int month, int currentYear)
        {
            var costs = this.recordRepository.AllAsNoTracking()
               .Where(x => x.Transfer.TransferType == TransferType.Payment & x.Transfer.TransferDate.Month == month & x.Transfer.TransferDate.Year == currentYear
               & x.UserId == userId);



            var groupedcosts = costs.GroupBy(x => x.MemberId)
                 .Select(x => new CostViewModel
                 {
                     MemberName = this.memberRepository.AllAsNoTracking().Where(y => y.Id == x.Key).FirstOrDefault().Name,
                     ItemValue = Math.Round((double)x.Sum(c => c.ItemValue), 2),
                 })
                 .OrderBy(x=>x.MemberName)
                 .ToList();
            return groupedcosts;
        }

        public List<CostViewModel> AllIncomesByMember(string userId, int month, int currentYear)
        {
            var incomes = this.recordRepository.AllAsNoTracking()
               .Where(x => x.Transfer.TransferType == TransferType.Income & x.Transfer.TransferDate.Month == month & x.Transfer.TransferDate.Year==currentYear
               & x.UserId == userId);



            var groupedcosts = incomes.GroupBy(x => x.MemberId)
                 .Select(x => new CostViewModel
                 {
                     MemberName = this.memberRepository.AllAsNoTracking().Where(y => y.Id == x.Key).FirstOrDefault().Name,
                     ItemValue = Math.Round((double)x.Sum(c => c.ItemValue), 2),
                 })
                 .OrderBy(x=>x.MemberName)
                 .ToList();
            return groupedcosts;
        }

        public double NikisCosts(string userId, int month, int currentYear)
        {
            var motherFatherCosts = Convert.ToDouble(this.AllCostsByMember(userId, month, currentYear).Where(x => x.MemberName == "Майка и татко").Select(x => x.ItemValue).FirstOrDefault().ToString());
            var motherFatherIncomes = Convert.ToDouble(this.AllIncomesByMember(userId, month, currentYear).Where(x => x.MemberName == "Майка и татко").Select(x=>x.ItemValue).FirstOrDefault().ToString());
            var nikiCosts = Convert.ToDouble(this.AllCostsByMember(userId, month, currentYear).Where(x => x.MemberName == "Ники").Select(x=>x.ItemValue).FirstOrDefault().ToString());
            var nikiIncomes = Convert.ToDouble(this.AllIncomesByMember(userId, month, currentYear).Where(x => x.MemberName == "Ники").Select(x => x.ItemValue).FirstOrDefault().ToString());
            var familyCosts = Convert.ToDouble(this.AllCostsByMember(userId, month, currentYear).Where(x => x.MemberName == "Семейство").Select(x => x.ItemValue).FirstOrDefault().ToString());
            var topchoCosts = Convert.ToDouble(this.AllCostsByMember(userId, month, currentYear).Where(x => x.MemberName == "Топчо").Select(x => x.ItemValue).FirstOrDefault().ToString());
            var sumToPay = (nikiIncomes - nikiCosts) - ((motherFatherCosts - motherFatherIncomes + topchoCosts + familyCosts) /2);
            return sumToPay;
        }

        public List<IncomeCostsViewModel> GetAll(string userId, int currentYear)
        {
            int month = DateTime.Now.Month;
            var incomesCostsModel = new List<IncomeCostsViewModel>();
            var incomeSum = this.transferRepository.AllAsNoTracking().Where(x => x.UserId == userId & x.TransferType == TransferType.Income & x.TransferDate.Year == currentYear);
            var costsSum = this.transferRepository.AllAsNoTracking().Where(x => x.UserId == userId & x.TransferType == TransferType.Payment & x.TransferDate.Year == currentYear);
            for (int i = 1; i <= 12; i++)
            {
                var incomeCostModel = new IncomeCostsViewModel();
                var totalIncomeSum = incomeSum.Where(x=>x.TransferDate.Month==i).Select(x => x.TotalMoney).Sum();
                var totalCostsSum = costsSum
                    .Where(x => x.TransferDate.Month == i )
                    .Select(x => x.TotalMoney)
                    .Sum();
                incomeCostModel.TotalCosts = Convert.ToDouble(totalCostsSum);
                incomeCostModel.TotalIncomes = Convert.ToDouble(totalIncomeSum);
                incomeCostModel.Month = i;
                incomeCostModel.Difference = Math.Round(incomeCostModel.TotalIncomes - incomeCostModel.TotalCosts, 2);
                incomeCostModel.AllCostsByMember = this.AllCostsByMember(userId, i, currentYear);
                incomeCostModel.AllIncomesByMember = this.AllIncomesByMember(userId, i, currentYear);
                incomesCostsModel.Add(incomeCostModel);
                incomeCostModel.NikiToPay = this.NikisCosts(userId, i, currentYear);

            }
            return incomesCostsModel.ToList();

        }
    }
}
