namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;

    public interface ITransferTypeService
    {
        ICollection<string> GetAllTransferTypes();
    }
}
