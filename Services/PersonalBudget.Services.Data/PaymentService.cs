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
    using PersonalBudget.Web.ViewModels.Payments;

    public class PaymentService : IPaymentService
    {
        private readonly IDeletableEntityRepository<Payment> paymentRepository;
        private readonly IDeletableEntityRepository<Transfer> transferRepository;
        private readonly IDeletableEntityRepository<FinanceInstitution> financeInstitutionRepository;

        public PaymentService(
            IDeletableEntityRepository<Payment> paymentRepository,
            IDeletableEntityRepository<Transfer> transferRepository,
            IDeletableEntityRepository<FinanceInstitution> financeInstitutionRepository)
        {
            this.paymentRepository = paymentRepository;
            this.transferRepository = transferRepository;
            this.financeInstitutionRepository = financeInstitutionRepository;
        }

        public List<PaymentViewModel> PaymentsAfterSearch<T>(string userId, string searchTerm, string initialDate, string finalDate, int financeInstitutionId)
        {
            var payments = this.paymentRepository.AllAsNoTracking()
            .Where(x => x.UserId == userId & x.Transfer.TransferType == TransferType.Payment);
            if (initialDate != null & finalDate != null)
            {
                payments = payments.Where(x => x.CreatedOn.Date >= Convert.ToDateTime(initialDate) && x.CreatedOn.Date <= Convert.ToDateTime(finalDate));
            }

            if (financeInstitutionId > 0)
            {
                payments = payments.Where(x => x.FinanceInstitutionId == financeInstitutionId);
            }

            return payments.To<PaymentViewModel>().ToList();
        }

        public async Task CreatePayment(CreatePaymentInputModel createPayment, string userId)
        {
            var transfer = this.transferRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == createPayment.TransferId);
            transfer.RemainingAmount = decimal.Parse(createPayment.RemainingSum, CultureInfo.InvariantCulture);
            transfer.PaidSum = decimal.Parse(createPayment.PaidSum, CultureInfo.InvariantCulture);
            this.transferRepository.Update(transfer);
            await this.transferRepository.SaveChangesAsync();
            var payment = new Payment
            {
                PaidSum = decimal.Parse(createPayment.PaidSum, CultureInfo.InvariantCulture),
                FinanceTypeId = createPayment.FinanceTypeId,
                FinanceInstitutionId = createPayment.FinanceInstitutionId,
                TransferId = createPayment.TransferId,
                StartCapital = decimal.Parse(createPayment.StartCapital, CultureInfo.InvariantCulture),
                FinalCapital = decimal.Parse(createPayment.FinalCapital, CultureInfo.InvariantCulture),
                UserId = userId,
            };
            await this.paymentRepository.AddAsync(payment);
            await this.paymentRepository.SaveChangesAsync();
            transfer.Payments.Add(payment);
            var financeInstitution = this.financeInstitutionRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == createPayment.FinanceInstitutionId);
            financeInstitution.Capital = decimal.Parse(createPayment.FinalCapital, CultureInfo.InvariantCulture);
            this.financeInstitutionRepository.Update(financeInstitution);
            await this.financeInstitutionRepository.SaveChangesAsync();

            await this.transferRepository.SaveChangesAsync();
        }

        public List<PaymentViewModel> GetAll(string userId, string searchTerm, string initialDate, string finalDate, int page, int financeInstitutionId, int itemPerPage = 5)
        {
            var payments = this.PaymentsAfterSearch<PaymentViewModel>(userId, searchTerm, initialDate, finalDate, financeInstitutionId)
                .OrderBy(x => x.CreatedOn)
                 .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage)
                .ToList();
            return payments;
        }

        public int TotalCount(string userId, string searchTerm, string initialDate, string finalDate, int financeInstitutionId)
        {
            int paymentsCount = this.PaymentsAfterSearch<PaymentViewModel>(userId, searchTerm, initialDate, finalDate, financeInstitutionId).Count;
            return paymentsCount;
        }
    }
}
