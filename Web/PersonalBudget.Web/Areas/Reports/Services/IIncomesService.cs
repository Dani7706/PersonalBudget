namespace PersonalBudget.Web.Areas.Reports.Services
{
    using System.Collections.Generic;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Web.Areas.Reports.Models;

    public interface IIncomesService
    {
        List<CostViewModel> GetAll(string userId, string searchTerm, string initialDate, string finalDate, int page, int subCategoryId, int itemPerPage = 5);

        decimal TotalIncomes(string userId);

        int TotalCount(string userId, string searchTerm, string initialDate, string finalDate, int page, int subCategoryId, int itemPerPage = 5);

        List<Record> IncomesAfterSearch<T>(string userId, string searchTerm, string initialDate, string finalDate, int subCategoryId);
    }
}
