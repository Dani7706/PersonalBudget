namespace PersonalBudget.Web.ViewModels.Towns
{
    using System.Collections.Generic;

    public class AllTownsViewModel : PagingViewModel
    {
        public IEnumerable<TownViewModel> Towns { get; set; }
    }
}
