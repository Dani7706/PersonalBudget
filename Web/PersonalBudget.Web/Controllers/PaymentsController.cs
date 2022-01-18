namespace PersonalBudget.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.ViewModels.Payments;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly IFinanceTypeService financeType;
        private readonly IFinanceInstitutionService financeInstitution;
        private readonly IPaymentService paymentService;

        public PaymentsController(IFinanceTypeService financeType, IFinanceInstitutionService financeInstitution, IPaymentService paymentService)
        {
            this.financeType = financeType;
            this.financeInstitution = financeInstitution;
            this.paymentService = paymentService;
        }

        public IActionResult CreatePayment()
        {
            string userId = this.User.GetId();
            var model = new CreatePaymentInputModel
            {
                FinanceTypes = this.financeType.GetAllFinanceTypes(),
                FinanceInstitutions = this.financeInstitution.GetAllFinanceInstitutions(userId),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentInputModel paymentInputModel)
        {
            string userId = this.User.GetId();
            var errors = this.ModelState.Values.SelectMany(v => v.Errors);
            if (!this.ModelState.IsValid)
            {
                paymentInputModel.FinanceTypes = this.financeType.GetAllFinanceTypes();
                paymentInputModel.FinanceInstitutions = this.financeInstitution.GetAllFinanceInstitutions(userId);
                return this.View(paymentInputModel);
            }

            await this.paymentService.CreatePayment(paymentInputModel, userId);
            this.TempData["Message"] = "Плащането бе успешно.";

            return this.RedirectToAction("All");
        }

        public IActionResult All([FromQuery] AllPaymentsViewModel allPayments)
        {
            string userId = this.User.GetId();
            int itemsPerPage = allPayments.ItemsPerPage;
            if (itemsPerPage == 0)
            {
                itemsPerPage = 10;
            }

            var payments = this.paymentService.GetAll(userId, allPayments.SearchTerm, allPayments.InitialDate, allPayments.FinalDate, allPayments.PageNumber, allPayments.FinanceInstitutionId, itemsPerPage);
            allPayments.ItemsPerPage = itemsPerPage;
            allPayments.Count = this.paymentService.TotalCount(userId, allPayments.SearchTerm, allPayments.InitialDate, allPayments.FinalDate, allPayments.FinanceInstitutionId);
            allPayments.FinanceInstitutions = this.financeInstitution.GetAllFinanceInstitutions(userId);
            allPayments.Payments = payments;
            var totalPaidSum = payments.Select(x => x.PaidSum).Sum();
            allPayments.TotalPaidSum = totalPaidSum;
            return this.View(allPayments);
        }
    }
}
