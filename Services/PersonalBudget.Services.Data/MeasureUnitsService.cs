namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.MeasureUnits;

    public class MeasureUnitsService : IMeasureUnitsService
    {
        private readonly IDeletableEntityRepository<MeasureUnit> measureUnitRepository;

        public MeasureUnitsService(IDeletableEntityRepository<MeasureUnit> measureUnitRepository)
        {
            this.measureUnitRepository = measureUnitRepository;
        }

        public async Task CreateMeasureUnit(CreateMeasureUnitInputModel createMeasureUnitInputModel)
        {
            var measureUnit = new MeasureUnit
            {
                Name = createMeasureUnitInputModel.Name,
                ShortName = createMeasureUnitInputModel.ShortName,
            };

            await this.measureUnitRepository.AddAsync(measureUnit);
            await this.measureUnitRepository.SaveChangesAsync();
        }

        public bool Exists(string shortName, string name)
        {
            var measureUnit = this.measureUnitRepository.AllAsNoTracking()
               .Where(x => x.Name == name & x.ShortName == shortName).FirstOrDefault();
            if (measureUnit != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<MeasureUnitViewModel> GetAll()
        {
            return this.measureUnitRepository.AllAsNoTracking()
                .To<MeasureUnitViewModel>()
                .ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllMeasureUnits()
        {
            return this.measureUnitRepository.All().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
