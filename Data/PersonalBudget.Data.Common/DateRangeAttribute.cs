namespace PersonalBudget.Data.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute()
           : base(typeof(DateTime), DateTime.Now.AddYears(-2).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), DateTime.UtcNow.AddYears(5).ToString("dd/MM/yyyy"))
        {
        }
    }
}
