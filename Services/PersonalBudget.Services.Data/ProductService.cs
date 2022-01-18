namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.Products;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Item> items;

        public ProductService(IDeletableEntityRepository<Item> items)
        {
            this.items = items;
        }

        public async Task CreateProduct(CreateProductInputModel createProduct, string userId)
        {
            var product = new Item
            {
                Name = createProduct.Name,
                SubCategoryId = createProduct.SubCategoryId,
                UserId = userId,
            };
            await this.items.AddAsync(product);
            await this.items.SaveChangesAsync();
        }

        public async Task Delete(string userId, int id)
        {
            var item = this.items.AllAsNoTracking()
                .FirstOrDefault(x => x.UserId == userId & x.Id == id);
            this.items.Delete(item);
            await this.items.SaveChangesAsync();
        }

        public async Task Edit(EditViewModel model, string userId, int id)
        {
            var item = this.items.AllAsNoTracking()
                .FirstOrDefault(x => x.UserId == userId & x.Id == id);
            item.Name = model.Name;
            item.SubCategoryId = model.SubCategoryId;
            this.items.Update(item);
            await this.items.SaveChangesAsync();
        }

        public bool Exist(string userId, string name, int subcategoryId)
        {
            var product = this.items.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Name == name & x.SubCategoryId == subcategoryId).FirstOrDefault();
            if (product == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public ItemsQueryModel GetAll(string userId, int itemsPerPage, string searchTerm, int subCategoryId = 0, int pageNumber = 1)
        {
            var itemsList = this.items.AllAsNoTracking()
                .Where(x => x.UserId == userId);
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                itemsList = itemsList.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (subCategoryId > 0)
            {
                itemsList = itemsList.Where(x => x.SubCategoryId == subCategoryId);
            }

            var totalItems = itemsList.Count();
            var productsList = itemsList
                .OrderBy(x => x.Name)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<ItemViewModel>()
                .ToList();
            return new ItemsQueryModel()
            {
                TotalItems = totalItems,
                Items = productsList,
                ItemsPerPage = itemsPerPage,
                PageNumber = pageNumber,
            };
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllProducts(string userId)
        {
            return this.items.All()
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public T GetById<T>(string userId, int id)
        {
            var item = this.items.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id)
                .To<T>()
                .FirstOrDefault();
            return item;
        }

        public int GetCount(string userId)
        {
            var count = this.items.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .Count();
            return count;
        }
    }
}
