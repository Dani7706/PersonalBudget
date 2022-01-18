namespace PersonalBudget.Web.ViewModels.Items
{
    using System.Collections.Generic;

    public class CreateItemInputModel
    {
        public string Item { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
    }
}
