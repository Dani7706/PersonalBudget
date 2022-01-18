namespace PersonalBudget.Web.ViewModels.MeasureUnits
{
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    public class MeasureUnitViewModel : IMapFrom<MeasureUnit>
    {
        public int Id { get; set; }

        public string ShortName { get; set; }

        public string Name { get; set; }
    }
}
