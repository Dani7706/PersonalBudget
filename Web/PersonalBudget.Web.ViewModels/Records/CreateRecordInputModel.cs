namespace PersonalBudget.Web.ViewModels.Records
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Common;

    using static PersonalBudget.Data.DataConstants.Record;

    public class CreateRecordInputModel
    {
        [Display(Name = "Продукт/ услуга")]

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

        [Display(Name = "Ед. цена")]
        [Required]
        [Range(MinUnitPrice, MaxUnitPrice)]
        public string UnitPrice { get; set; }

        [Display(Name = "Потребител")]
        public int MemberId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Members { get; set; }

        [DateRangeAttribute]
        [Display(Name = "Срок на годност")]
        public DateTime? ExpireDate { get; set; }

        [Display(Name = "Стойност")]
        [Required]
        [Range(MinItemValue, MaxItemValue)]
        public string ItemValue { get; set; }

        public int TransferId { get; set; }
    }
}
