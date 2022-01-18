namespace PersonalBudget.Services.Data
{
    using System.Threading.Tasks;

    using PersonalBudget.Web.ViewModels.Records;

    public interface IRecordsService
    {
        Task CreateRecord(CreateRecordInputModel createRecord, string userId);

        Task EditRecord(RecordViewModel editRecord, string userId, int id);

        T GetById<T>(string userId, int id);

        int GetTransferId(string userId, int id);

        int GetCount(string userId);
    }
}
