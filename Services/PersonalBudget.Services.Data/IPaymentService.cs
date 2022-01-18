namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PersonalBudget.Web.ViewModels.Payments;

    public interface IPaymentService
    {
        Task CreatePayment(CreatePaymentInputModel createPayment, string userId);

        List<PaymentViewModel> GetAll(string userId, string searchTerm, string initialDate, string finalDate, int page, int financeInstitutionId, int itemPerPage = 5);

        int TotalCount(string userId, string searchTerm, string initialDate, string finalDate, int financeInstitutionId);

        List<PaymentViewModel> PaymentsAfterSearch<T>(string userId, string searchTerm, string initialDate, string finalDate, int financeInstitutionId);
    }
}
