namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;

    public class FinanceTypeService : IFinanceTypeService
    {
        private readonly IDeletableEntityRepository<FinanceType> financeTypesRepository;

        public FinanceTypeService(IDeletableEntityRepository<FinanceType> financeTypesRepository)
        {
            this.financeTypesRepository = financeTypesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllFinanceTypes()
        {
            return this.financeTypesRepository.All()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
