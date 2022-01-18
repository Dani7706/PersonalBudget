namespace PersonalBudget.Web.ViewModels.Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;

    using static PersonalBudget.Data.DataConstants.Record;

    public class EditViewModel : IMapFrom<Record>
    {
        public int Id { get; set; }

        public int TransferId { get; set; }

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

        [Display(Name = "Потребител")]
        public int MemberId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Members { get; set; }

        [Display(Name = "Срок на годност")]
        [DataType(DataType.Date)]

        // [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpireDate { get; set; }
    }
}
