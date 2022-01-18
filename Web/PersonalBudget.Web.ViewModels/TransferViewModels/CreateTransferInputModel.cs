namespace PersonalBudget.Web.ViewModels.TransferViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Web.ViewModels.Records;

    using static PersonalBudget.Data.DataConstants.Transfer;

    public class CreateTransferInputModel
    {
        [Display(Name = "№ трансфер")]
        public int TransferNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Дата трансфер (месец/ден/година)")]
        public DateTime TransferDate { get; set; }

        [Display(Name = "Контрагент")]
        public int PartnerId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Partners { get; set; }

        [Display(Name = "Вид трансфер")]
        public string TransferType { get; set; }

        [Display(Name = "Вид трансфер")]
        public TransferType Type { get; set; }

        [Required]
        [Range(MinCapitalValue, MaxCapitalValue)]
        [Display(Name = "Общо сума")]
        public string TotalMoney { get; set; }

        [Display(Name = "Продукт/услуга")]
        public int ItemId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Items { get; set; }

        [Display(Name = "Подкатегория")]

        public int SubCategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Subcategories { get; set; }

        [Required]
        [Range(MinQuantity, MaxQuantity)]
        [Display(Name = "Количество")]
        public string Quantity { get; set; }

        [Display(Name = "Мярка")]

        public int MeasureUnitId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> MeasureUnits { get; set; }

        [Display(Name = "Ед. цена без ДДС")]
        public string UnitPriceNoVat { get; set; }

        [Required]
        [Range(MinUnitPrice, MaxUnitPrice)]
        [Display(Name = "Ед. цена")]
        public string UnitPrice { get; set; }

        [Required]
        [Range(MinItemValue, MaxItemValue)]
        [Display(Name = "Стойност")]
        public string ItemValue { get; set; }

        [Display(Name = "Потребител")]

        public int MemberId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Members { get; set; }

        [DateRangeAttribute]
        [Display(Name = "Срок на годност(м/д/г)")]
        public DateTime? ExpireDate { get; set; }

        public IEnumerable<CreateRecordInputModel> Records { get; set; }
    }
}
