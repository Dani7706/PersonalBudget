namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;

    // using System.Web.Mvc;
    public interface IFinanceTypeService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllFinanceTypes();
    }
}
