namespace PersonalBudget.Web.Areas.Reports.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Web.Areas.Reports.Models;

    public class IncomesService : IIncomesService
    {
        private readonly IDeletableEntityRepository<Transfer> transferRepository;
        private readonly IDeletableEntityRepository<Partner> partnerRepository;
        private readonly IDeletableEntityRepository<SubCategory> subcategoryRepository;
        private readonly IDeletableEntityRepository<Member> memberRepository;
        private readonly IDeletableEntityRepository<Record> recordRepository;

        public IncomesService(
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
        }

        public List<CostViewModel> GetAll(string userId, string searchTerm, string initialDate, string finalDate, int page, int subCategoryId, int itemPerPage = 5)
        {
            var totalIncomeSum = this.transferRepository.AllAsNoTracking()
    .Where(x => x.UserId == userId & x.TransferType == TransferType.Income)
    .Select(x => x.TotalMoney)
    .Sum();

            var records = this.IncomesAfterSearch<Record>(userId, searchTerm, initialDate, finalDate, subCategoryId);
            var incomesModel = new List<CostViewModel>();
            foreach (var record in records)
            {
                int partnerId = (int)this.transferRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == record.TransferId).PartnerId;
                int incomesPercent = (int)(Convert.ToDecimal(record.ItemValue) / totalIncomeSum * 100);
                string date = this.transferRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == record.TransferId).TransferDate.ToString();
                var costModel = new CostViewModel()
                {
                    PartnerName = this.partnerRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == partnerId).Name,
                    ItemValue = (double)record.ItemValue,
                    MemberName = this.memberRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == record.MemberId).Name,
                    SubcategoryName = this.subcategoryRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == record.SubCategoryId).Name,
                    IncomePercent = incomesPercent,
                    Date = date,
                };
                incomesModel.Add(costModel);
            }

            return incomesModel;
        }

        public List<Record> IncomesAfterSearch<T>(string userId, string searchTerm, string initialDate, string finalDate, int subCategoryId)
        {
            var records = this.recordRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Transfer.TransferType == TransferType.Income);
            if (initialDate != null & finalDate != null)
            {
                records = records.Where(x => x.Transfer.TransferDate.Date >= Convert.ToDateTime(initialDate) && x.Transfer.TransferDate.Date <= Convert.ToDateTime(finalDate));
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                records = records.Where(x => x.Transfer.Partner.Name.Contains(searchTerm) || x.Transfer.Records.All(y => y.Member.Name.Contains(searchTerm)));
            }

            if (subCategoryId > 0)
            {
                records = records.Where(x => x.Transfer.Records.All(y => y.SubCategoryId == subCategoryId));
            }

            return records.ToList();
        }

        public int TotalCount(string userId, string searchTerm, string initialDate, string finalDate, int page, int subCategoryId, int itemPerPage = 5)
        {
            var records = this.IncomesAfterSearch<Record>(userId, searchTerm, initialDate, finalDate, subCategoryId).Count;
            return records;
        }

        public decimal TotalIncomes(string userId)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            var totalIncomeSum = this.transferRepository.AllAsNoTracking()
   .Where(x => x.UserId == userId & x.TransferType == TransferType.Income & x.TransferDate.Month == month & x.TransferDate.Year==year)
   .Select(x => x.TotalMoney)
   .Sum();
            return totalIncomeSum;
        }
    }
}
