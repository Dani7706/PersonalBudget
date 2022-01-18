namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.Towns;

    public class TownsService : ITownsService
    {
        private readonly IDeletableEntityRepository<Town> townRepository;

        public TownsService(IDeletableEntityRepository<Town> townRepository)
        {
            this.townRepository = townRepository;
        }

        public int Count(string userId)
        {
            return this.townRepository.AllAsNoTracking().Where(x => x.UserId == userId).Count();
        }

        public async Task Create(CreateTownInputModel createTown, string userId)
        {
            var town = new Town
            {
                Name = createTown.Name,
                Country = createTown.Country,
                UserId = userId,
            };

            await this.townRepository.AddAsync(town);
            await this.townRepository.SaveChangesAsync();
        }

        public async Task Delete(string userId, int id)
        {
            var town = this.townRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id).FirstOrDefault();
            this.townRepository.Delete(town);
            await this.townRepository.SaveChangesAsync();
        }

        public async Task Edit(TownViewModel editTown, string userId, int id)
        {
            var town = this.townRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id).FirstOrDefault();
            town.Country = editTown.Country;
            town.Name = editTown.Name;
            this.townRepository.Update(town);
            await this.townRepository.SaveChangesAsync();
        }

        public bool Exists(string userId, string name, string country)
        {
            var town = this.townRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Name == name & x.Country == country).FirstOrDefault();
            if (town != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<TownViewModel> GetAll(string userId, int currentPage, int itemsPerPage)
        {
            return this.townRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Name)
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<TownViewModel>()
                .ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllTowns(string userId)
        {
            return this.townRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public T GetById<T>(string userId, int id)
        {
            var town = this.townRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id)
                .To<T>()
                .FirstOrDefault();
            return town;
        }
    }
}
