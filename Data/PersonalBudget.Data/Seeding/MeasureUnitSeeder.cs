namespace PersonalBudget.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using PersonalBudget.Data;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Data.Seeding;

    internal class MeasureUnitSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.MeasureUnits.Any())
            {
                return;
            }

            await dbContext.MeasureUnits.AddAsync(new MeasureUnit { Name = "Килограм", ShortName = "Kg" });
            await dbContext.MeasureUnits.AddAsync(new MeasureUnit { Name = "Брой", ShortName = "Nr" });
            await dbContext.MeasureUnits.AddAsync(new MeasureUnit { Name = "Литър", ShortName = "L" });
            await dbContext.MeasureUnits.AddAsync(new MeasureUnit { Name = "Милилитър", ShortName = "ml" });
            await dbContext.MeasureUnits.AddAsync(new MeasureUnit { Name = "Грам", ShortName = "gr" });
        }
    }
}
