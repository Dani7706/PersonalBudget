namespace PersonalBudget.Web.ViewModels.TransferViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    using static PersonalBudget.Data.DataConstants.Transfer;

    public class TransferViewModel : IMapFrom<Transfer>
    {
        [Display(Name = "№ трансфер")]
        public int Id { get; set; }

        [Display(Name = "Вид трансфер")]
        public string TransferType { get; set; }

        [Display(Name = "Контрагент")]
        public string PartnerName { get; set; }

        [Required]
        [DateRangeAttribute]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Дата трансфер")]
        public DateTime TransferDate { get; set; }

        [Required]
        [Range(MinCapitalValue, MaxCapitalValue)]
        [Display(Name = "Общо сума")]
        public decimal TotalMoney { get; set; }

        public decimal PaidSum { get; set; }

        public decimal RemainingAmount { get; set; }
    }
}
