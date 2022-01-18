namespace PersonalBudget.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PagingViewModel
    {
        public int PageNumber { get; set; } = 1;

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.Count / this.ItemsPerPage);

        public int Count { get; set; }

        [Display(Name = "брой на страница")]
        public int ItemsPerPage { get; set; }
    }
}
