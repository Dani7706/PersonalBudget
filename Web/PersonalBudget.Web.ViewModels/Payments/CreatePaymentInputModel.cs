namespace PersonalBudget.Web.ViewModels.Payments
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;

    public class CreatePaymentInputModel
    {
        public string TransferType { get; set; }

        [Display(Name = "Вид движение")]
        public TransferType Type { get; set; }

        [Display(Name = "№ трансфер")]
        public int TransferId { get; set; }

        [Display(Name = "Финансови средства")]
        public int FinanceTypeId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> FinanceTypes { get; set; }

        [Required]
        [Display(Name = "Финансова институция")]
        public int FinanceInstitutionId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> FinanceInstitutions { get; set; }

        [Required]
        [Range(0.00, 1000000.00)]
        [Display(Name = "Сума за плащане/получаване")]
        public string SumToPay { get; set; }

        [Required]
        [Display(Name = "Начален капитал")]
        public string StartCapital { get; set; }

        [Required]
        [Range(0.00, 1000000.00)]
        [Display(Name = "Платена сума")]
        public string PaidSum { get; set; }

        [Display(Name = "Краен капитал")]
        public string FinalCapital { get; set; }

        [Required]
        [Range(0.00, 1000000.00)]
        [Display(Name = "Оставаща сума за плащане")]
        public string RemainingSum { get; set; }
    }
}
