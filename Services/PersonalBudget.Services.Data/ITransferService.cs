namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PersonalBudget.Web.ViewModels.TransferViewModels;

    public interface ITransferService
    {
        Task CreateTransfer(CreateTransferInputModel createTransfer, string userId);

        IEnumerable<TransferViewModel> GetAll<T>(int page, string userId, string search, int partnerId, int itemsPerPage = 12);

        int GetCount(string userId, string search, int partnerId);

        T GetById<T>(string userId, int id);

        Task Edit(string userId, int id, EditTransferViewModel editTransfer);

        Task Delete(string userId, int id);

        List<T> TransfersAfterSearch<T>(string userId, string search, int partnerId);
    }
}
