namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.TransferViewModels;

    public class TransferService : ITransferService
    {
        private readonly IDeletableEntityRepository<Transfer> transferRepository;
        private readonly IDeletableEntityRepository<Record> recordRepository;

        public TransferService(IDeletableEntityRepository<Transfer> transferRepository, IDeletableEntityRepository<Record> recordRepository)
        {
            this.transferRepository = transferRepository;
            this.recordRepository = recordRepository;
        }

        public async Task CreateTransfer(CreateTransferInputModel createTransfer, string userId)
        {
            var transfer = new Transfer
            {
                TransferDate = createTransfer.TransferDate,
                PartnerId = createTransfer.PartnerId,
                TransferType = createTransfer.Type,
                TotalMoney = decimal.Parse(createTransfer.TotalMoney, System.Globalization.CultureInfo.InvariantCulture),
                RemainingAmount = decimal.Parse(createTransfer.TotalMoney, System.Globalization.CultureInfo.InvariantCulture),
                UserId = userId,
            };
            await this.transferRepository.AddAsync(transfer);
            await this.transferRepository.SaveChangesAsync();
            int transferId = this.transferRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId).OrderBy(y=>y.Id).LastOrDefault().Id;
            foreach (var inputRecord in createTransfer.Records)
            {
                var record = new Record
                {
                    MeasureUnitId = inputRecord.MeasureUnitId,
                    MemberId = inputRecord.MemberId,
                    ItemId = inputRecord.ItemId,
                    Quantity = double.Parse(inputRecord.Quantity, System.Globalization.CultureInfo.InvariantCulture),
                    UnitPrice = double.Parse(inputRecord.UnitPrice, System.Globalization.CultureInfo.InvariantCulture),
                    SubCategoryId = inputRecord.SubCategoryId,
                    ItemValue = double.Parse(inputRecord.ItemValue, System.Globalization.CultureInfo.InvariantCulture),
                    ExpireDate = inputRecord.ExpireDate,
                    TransferId = transferId,
                    UserId = userId,
                };
                await this.recordRepository.AddAsync(record);
                await this.recordRepository.SaveChangesAsync();
                transfer.Records.Add(record);
                await this.transferRepository.SaveChangesAsync();
            }

            // await this.transferRepository.AddAsync(transfer);
            // await this.transferRepository.SaveChangesAsync();
        }

        public async Task Delete(string userId, int id)
        {
            var transfer = this.transferRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id)
                .FirstOrDefault();
            this.transferRepository.Delete(transfer);
            await this.transferRepository.SaveChangesAsync();
        }

        public async Task Edit(string userId, int id, EditTransferViewModel editTransfer)
        {
            var transfer = this.transferRepository.All()
                 .Where(x => x.UserId == userId & x.Id == id)
               .FirstOrDefault();
            transfer.PartnerId = editTransfer.PartnerId;
            transfer.TransferDate = editTransfer.TransferDate;
            transfer.TransferType = editTransfer.Type;

            transfer.TotalMoney = decimal.Parse(editTransfer.TotalMoney, System.Globalization.CultureInfo.InvariantCulture);
            await this.transferRepository.SaveChangesAsync();
        }

        public IEnumerable<TransferViewModel> GetAll<T>(int page, string userId, string search, int partnerId, int itemsPerPage = 12)
        {
            var transferList = this.TransfersAfterSearch<TransferViewModel>(userId, search, partnerId);

            var transfers = transferList.OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            return transfers;
        }

        public T GetById<T>(string userId, int id)
        {
            var transfer = this.transferRepository.AllAsNoTracking()
               .Where(x => x.UserId == userId & x.Id == id)
               .To<T>()
               .FirstOrDefault();

            return transfer;
        }

        public int GetCount(string userId, string search, int partnerId)
        {
            int transfersCount = this.TransfersAfterSearch<TransferViewModel>(userId, search, partnerId).Count();
            return transfersCount;
        }

        public List<T> TransfersAfterSearch<T>(string userId, string search, int partnerId)
        {
            var transferList = this.transferRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId);
            if (!string.IsNullOrWhiteSpace(search))
            {
                if ("доход".Contains(search.ToLower()))
                {
                    transferList = transferList.Where(x => x.TransferType == TransferType.Income);
                }
                else if ("плащане".Contains(search.ToLower()))
                {
                    transferList = transferList.Where(x => x.TransferType == TransferType.Payment);
                }
                else
                {
                    transferList = transferList.Where(x => x.TransferType == TransferType.Other);
                }
            }

            if (partnerId > 0)
            {
                transferList = transferList.Where(x => x.PartnerId == partnerId);
            }

            return transferList.To<T>().ToList();
        }
    }
}
