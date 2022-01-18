namespace PersonalBudget.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using PersonalBudget.Data;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Data.Seeding;

    internal class FinanceTypeSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.FinanceTypes.Any())
            {
                return;
            }

            await dbContext.FinanceTypes.AddAsync(new FinanceType { Name = "Каса" });
            await dbContext.FinanceTypes.AddAsync(new FinanceType { Name = "Банка" });
            await dbContext.FinanceTypes.AddAsync(new FinanceType { Name = "Ваучери" });
        }
    }
}
