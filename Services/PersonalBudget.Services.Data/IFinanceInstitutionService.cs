namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Web.ViewModels.FinanceInstitutions;

    public interface IFinanceInstitutionService
    {
        Task CreateFinanceInstitutionsAsync(CreateFinanceInstitutionInputModel model, string userId);

        IEnumerable<KeyValuePair<string, string>> GetAllFinanceInstitutions(string userId);

        IEnumerable<FinanceInstitutionViewModel> GetAll(string userId);

        IList<FinanceInstitution> GetAllFinanceInstitutions(string userId, int financeTypeId);

        T GetFinanceInstitutionById<T>(string userId, int financeInstitutionId);

        Task Edit(string userId, int id, EditViewModel institutionInputModel);

        Task Delete(string userId, int id);

        bool Exists(string userId, string name, int type);
    }
}
