namespace PersonalBudget.Services.Data
{
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.Records;

    public class RecordsService : IRecordsService
    {
        private readonly IDeletableEntityRepository<Record> recordRepository;

        public RecordsService(IDeletableEntityRepository<Record> recordRepository)
        {
            this.recordRepository = recordRepository;
        }

        public async Task CreateRecord(CreateRecordInputModel createRecord, string userId)
        {
        }

        public async Task EditRecord(RecordViewModel editRecord, string userId, int id)
        {
            var record = this.recordRepository.All().FirstOrDefault(x => x.Id == id);
            int transferRecord = (int)record.TransferId;
            record.ItemId = editRecord.ItemId;
            record.ItemValue = double.Parse(editRecord.ItemValue, System.Globalization.CultureInfo.InvariantCulture);
            record.MeasureUnitId = editRecord.MeasureUnitId;
            record.MemberId = editRecord.MeasureUnitId;
            record.Quantity = double.Parse(editRecord.Quantity, CultureInfo.InvariantCulture);
            record.ExpireDate = editRecord.ExpireDate;
            record.SubCategoryId = editRecord.SubCategoryId;
            record.UnitPrice = double.Parse(editRecord.UnitPrice, CultureInfo.InvariantCulture);
            await this.recordRepository.SaveChangesAsync();
        }

        public T GetById<T>(string userId, int id)
        {
            var transfer = this.recordRepository.AllAsNoTracking()
               .Where(x => x.UserId == userId & x.Id == id)
               .To<T>()
               .FirstOrDefault();

            return transfer;
        }

        public int GetTransferId(string userId, int id)
        {
            var record = this.recordRepository.All().FirstOrDefault(x => x.UserId == userId & x.Id == id);
            int transferRecord = (int)record.TransferId;
            return transferRecord;
        }

        public int GetCount(string userId)
        {
            return this.recordRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId).Count();
        }
    }
}
