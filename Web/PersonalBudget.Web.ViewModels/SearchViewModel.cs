namespace PersonalBudget.Web.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SearchViewModel : PagingViewModel
    {
        [Display(Name = "Подкатегория")]
        public int SubCategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Subcategories { get; set; }

        [Display(Name = "Финансова институция")]
        public int FinanceInstitutionId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> FinanceInstitutions { get; set; }

        [Display(Name = "Търси")]
        public string SearchTerm { get; set; }

        [Display(Name = "Начална дата (месец/ден/година)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string InitialDate { get; set; }

        [Display(Name = "Крайна дата (месец/ден/година)")]
        [DataType(DataType.Date)]
        public string FinalDate { get; set; }
    }
}
