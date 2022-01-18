namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PersonalBudget.Web.ViewModels.Towns;

    public interface ITownsService
    {
        Task Create(CreateTownInputModel createTown, string userId);

        IEnumerable<TownViewModel> GetAll(string userId, int currentPage, int itemPerPage);

        IEnumerable<KeyValuePair<string, string>> GetAllTowns(string userId);

        T GetById<T>(string userId, int id);

        Task Edit(TownViewModel editTown, string userId, int id);

        Task Delete(string userId, int id);

        bool Exists(string userId, string name, string country);

        int Count(string userId);
    }
}
