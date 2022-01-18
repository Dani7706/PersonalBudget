namespace PersonalBudget.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.FinanceInstitutions;

    using static PersonalBudget.Data.Common.Exceptions;

    public class FinanceInstitutionsService : IFinanceInstitutionService
    {
        private readonly IDeletableEntityRepository<FinanceInstitution> financeInstitutionRepository;
        private readonly IDeletableEntityRepository<FinanceType> financeTypeRepository;

        public FinanceInstitutionsService(
            IDeletableEntityRepository<FinanceInstitution> financeInstitutionRepository,
            IDeletableEntityRepository<FinanceType> financeTypeRepository)
        {
            this.financeInstitutionRepository = financeInstitutionRepository;
            this.financeTypeRepository = financeTypeRepository;
        }

        public async Task CreateFinanceInstitutionsAsync(CreateFinanceInstitutionInputModel model, string userId)
        {
            var financeInstitution = new FinanceInstitution
            {
                Name = model.Name,
                FinanceTypeId = model.FinanceTypeId,
                Capital = decimal.Parse(model.Capital, System.Globalization.CultureInfo.InvariantCulture),
                UserId = userId,
            };

            await this.financeInstitutionRepository.AddAsync(financeInstitution);
            await this.financeInstitutionRepository.SaveChangesAsync();
        }

        public async Task Delete(string userId, int id)
        {
            var financeInstitution = this.financeInstitutionRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id).FirstOrDefault();
            if (financeInstitution == null)
            {
                throw new NullReferenceException(NotFoundExceptionMessage);
            }

            this.financeInstitutionRepository.Delete(financeInstitution);
            await this.financeInstitutionRepository.SaveChangesAsync();
        }

        public async Task Edit(string userId, int id, EditViewModel institutionInputModel)
        {
            var institution = this.financeInstitutionRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id).FirstOrDefault();
            if (institution == null)
            {
                throw new NullReferenceException(NotFoundExceptionMessage);
            }

            institution.Capital = decimal.Parse(institutionInputModel.Capital, CultureInfo.InvariantCulture);
            institution.Name = institutionInputModel.Name;
            institution.FinanceTypeId = institutionInputModel.FinanceTypeId;
            this.financeInstitutionRepository.Update(institution);
            await this.financeInstitutionRepository.SaveChangesAsync();
        }

        public bool Exists(string userId, string name, int type)
        {
            var financeInstitution = this.financeInstitutionRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Name == name & x.FinanceTypeId == type).FirstOrDefault();
            if (financeInstitution != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<FinanceInstitutionViewModel> GetAll(string userId)
        {
            return this.financeInstitutionRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .To<FinanceInstitutionViewModel>()
                .ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllFinanceInstitutions(string userId)
        {
            return this.financeInstitutionRepository.All()
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IList<FinanceInstitution> GetAllFinanceInstitutions(string userId, int financeTypeId)
        {
            return this.financeInstitutionRepository.AllAsNoTracking().Where(x => x.FinanceTypeId == financeTypeId && x.UserId == userId).ToList();
        }

        public T GetFinanceInstitutionById<T>(string userId, int financeInstitutionId)
        {
            var financeInstitution = this.financeInstitutionRepository.All()
                .Where(x => x.Id == financeInstitutionId & x.UserId == userId)
                .To<T>()
                .FirstOrDefault();

            return financeInstitution;
        }
    }
}
