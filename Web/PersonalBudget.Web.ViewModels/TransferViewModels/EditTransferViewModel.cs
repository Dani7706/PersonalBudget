namespace PersonalBudget.Web.ViewModels.TransferViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.Records;

    using static PersonalBudget.Data.DataConstants.Transfer;

    public class EditTransferViewModel : IMapFrom<Transfer>
    {
        [Display(Name = "№")]
        public int Id { get; set; }

        [Display(Name = "Вид")]
        public string TransferType { get; set; }

        [Display(Name = "Вид")]
        public TransferType Type { get; set; }

        [Display(Name = "Контрагент")]
        public int PartnerId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Partners { get; set; }

        [DateRangeAttribute]
        [DataType(DataType.Date)]
        [Display(Name = "Дата трансфер (месец/ден/година)")]
        public DateTime TransferDate { get; set; }

        [Required]
        [Range(MinCapitalValue, MaxCapitalValue)]
        [Display(Name = "Общо сума")]
        public string TotalMoney { get; set; }

        public IEnumerable<RecordViewModel> Records { get; set; }
    }
}
