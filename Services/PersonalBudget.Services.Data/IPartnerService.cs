namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PersonalBudget.Web.ViewModels.Partners;

    public interface IPartnerService
    {
        Task CreatePartner(CreatePartnerInputModel createPartner, string userId);

        IEnumerable<KeyValuePair<string, string>> GetAllPartners(string userId);

        IEnumerable<PartnerViewModel> GetAll(string userId, int page, int itemsPerPage, string search, int townId);

        T GetById<T>(string userId, int id);

        Task Edit(EditViewModel editPartner, string userId, int id);

        Task Delete(string userId, int id);

        int GetCount(string userId, string search, int townId);

        List<PartnerViewModel> GetPartners(string userId, string search, int townId);

        bool Exist(string userId, string name, int townId);
    }
}
