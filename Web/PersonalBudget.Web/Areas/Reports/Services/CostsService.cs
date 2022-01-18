namespace PersonalBudget.Web.Areas.Reports.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Web.Areas.Reports.Models;

    public class CostsService : ICostsService
    {
        private readonly IDeletableEntityRepository<Transfer> transferRepository;
        private readonly IDeletableEntityRepository<Partner> partnerRepository;
        private readonly IDeletableEntityRepository<SubCategory> subcategoryRepository;
        private readonly IDeletableEntityRepository<Member> memberRepository;
        private readonly IDeletableEntityRepository<Record> recordRepository;
        private int incomesPercent;

        public CostsService(
            IDeletableEntityRepository<Transfer> transferRepository,
            IDeletableEntityRepository<Partner> partnerRepository,
            IDeletableEntityRepository<SubCategory> subcategoryRepository,
            IDeletableEntityRepository<Member> memberRepository,
            IDeletableEntityRepository<Record> recordRepository)
        {
            this.transferRepository = transferRepository;
            this.partnerRepository = partnerRepository;
            this.subcategoryRepository = subcategoryRepository;
            this.memberRepository = memberRepository;
            this.recordRepository = recordRepository;
            this.incomesPercent = 0;
        }

        public List<Record> CostsAfterSearch<T>(string userId, string searchTerm, string initialDate, string finalDate, int subCategoryId)
        {
            var records = this.recordRepository.AllAsNoTracking()
            .Where(x => x.UserId == userId & x.Transfer.TransferType==TransferType.Payment);
            if (initialDate != null & finalDate != null)
            {
                records = records.Where(x => x.Transfer.TransferDate.Date >= Convert.ToDateTime(initialDate) && x.Transfer.TransferDate.Date <= Convert.ToDateTime(finalDate));
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                records = records.Where(x => x.Transfer.Partner.Name.Contains(searchTerm) || x.Member.Name.Contains(searchTerm) || x.SubCategory.Name.Contains(searchTerm));
            }

            if (subCategoryId > 0)
            {
                records = records.Where(x => x.SubCategoryId == subCategoryId);
            }

            return records.ToList();
        }

        public List<CostViewModel> GetAll(string userId, string searchTerm, string initialDate, string finalDate, int page, int subCategoryId, int itemPerPage = 5)
        {
            var totalIncomeSum = this.transferRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.TransferType == TransferType.Income)
                .Select(x => x.TotalMoney)
                .Sum();

            var totalCostsSum = this.transferRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.TransferType == TransferType.Payment)
                .Select(x => x.TotalMoney)
                .Sum();
            var records = this.CostsAfterSearch<Record>(userId, searchTerm, initialDate, finalDate, subCategoryId);

            var costsModel = new List<CostViewModel>();
            foreach (var record in records)
            {
                int partnerId = (int)this.transferRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == record.TransferId).PartnerId;
                int costsPercent = (int)(Convert.ToDecimal(record.ItemValue) / totalCostsSum * 100);

                if (totalIncomeSum > 0)
                {
                    this.incomesPercent = (int)(Convert.ToDecimal(record.ItemValue) / totalIncomeSum * 100);
                }

                string date = this.transferRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == record.TransferId).TransferDate.ToString();

                var costModel = new CostViewModel()
                {
                    PartnerName = this.partnerRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == partnerId).Name,
                    ItemValue = (double)record.ItemValue,
                    MemberName = this.memberRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == record.MemberId).Name,
                    SubcategoryName = this.subcategoryRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == record.SubCategoryId).Name,
                    CostsPercent = costsPercent,
                    IncomePercent = this.incomesPercent,
                    Date = date,
                };
                costsModel.Add(costModel);
            }

            var costs = costsModel.OrderBy(x => x.Date)
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage)
                .ToList();
            return costs;
        }

        public decimal TotalCosts(string userId)
        {
            var totalCostsSum = this.transferRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.TransferType == TransferType.Payment & x.TransferDate.Month == DateTime.Now.Month)
                .Select(x => x.TotalMoney)
                .Sum();
            totalCostsSum = Math.Round(totalCostsSum, 2);
            return totalCostsSum;
        }

        public int TotalCount(string userId, string searchTerm, string initialDate, string finalDate, int page, int subCategoryId, int itemPerPage = 5)
        {
            var records = this.CostsAfterSearch<Record>(userId, searchTerm, initialDate, finalDate, subCategoryId).Count;
            return records;
        }
    }
}
