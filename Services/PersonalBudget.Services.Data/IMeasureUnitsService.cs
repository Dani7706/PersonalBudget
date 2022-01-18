namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PersonalBudget.Web.ViewModels.MeasureUnits;

    public interface IMeasureUnitsService
    {
        Task CreateMeasureUnit(CreateMeasureUnitInputModel createMeasureUnitInputModel);

        IEnumerable<MeasureUnitViewModel> GetAll();

        IEnumerable<KeyValuePair<string, string>> GetAllMeasureUnits();

        bool Exists(string shortName, string name);
    }
}
