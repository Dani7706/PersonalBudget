namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.Partners;

    public class PartnerService : IPartnerService
    {
        private readonly IDeletableEntityRepository<Partner> partnerRepository;

        public PartnerService(IDeletableEntityRepository<Partner> partnerRepository)
        {
            this.partnerRepository = partnerRepository;
        }

        public async Task CreatePartner(CreatePartnerInputModel createPartner, string userId)
        {
            var partner = new Partner
            {
                Name = createPartner.Name,
                TownId = createPartner.TownId,
                UserId = userId,
            };
            await this.partnerRepository.AddAsync(partner);
            await this.partnerRepository.SaveChangesAsync();
        }

        public async Task Delete(string userId, int id)
        {
            var partner = this.partnerRepository.AllAsNoTracking()
                .Where(x => x.Id == id & x.UserId == userId).FirstOrDefault();
            this.partnerRepository.Delete(partner);
            await this.partnerRepository.SaveChangesAsync();
        }

        public async Task Edit(EditViewModel editPartner, string userId, int id)
        {
            var partner = this.partnerRepository.AllAsNoTracking()
                .Where(x => x.Id == id & x.UserId == userId).FirstOrDefault();
            partner.Name = editPartner.Name;
            partner.TownId = editPartner.TownId;
            this.partnerRepository.Update(partner);
            await this.partnerRepository.SaveChangesAsync();
        }

        public bool Exist(string userId, string name, int townId)
        {
            var partner = this.partnerRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Name == name & x.TownId == townId).FirstOrDefault();
            if (partner == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<PartnerViewModel> GetAll(string userId, int page, int itemsPerPage, string search, int townId)
        {
            var partners = this.GetPartners(userId, search, townId)
                .OrderBy(x => x.Name)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            return partners;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllPartners(string userId)
        {
            return this.partnerRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Name)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public T GetById<T>(string userId, int id)
        {
            var partner = this.partnerRepository.AllAsNoTracking()
                .Where(x => x.Id == id & x.UserId == userId)
                .To<T>()
                .FirstOrDefault();
            return partner;
        }

        public int GetCount(string userId, string search, int townId)
        {
            var partnersCount = this.GetPartners(userId, search, townId).Count;
            return partnersCount;
        }

        public List<PartnerViewModel> GetPartners(string userId, string search, int townId)
        {
            var partnersList = this.partnerRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId);
            if (!string.IsNullOrWhiteSpace(search))
            {
                partnersList = partnersList.Where(x => x.Name.ToLower().Contains(search));
            }

            if (townId > 0)
            {
                partnersList = partnersList.Where(x => x.TownId == townId);
            }

            return partnersList.To<PartnerViewModel>().ToList();
        }
    }
}
