namespace PersonalBudget.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Data.Models.Inventory;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.Inventory;

    public class InventoryService : IInventoryService
    {
        private readonly IDeletableEntityRepository<Record> recordRepository;
        private readonly IDeletableEntityRepository<SubCategory> subcategoryRepository;
        private readonly IDeletableEntityRepository<Item> itemRepository;
        private readonly IDeletableEntityRepository<MeasureUnit> measureUnitRepository;
        private readonly IDeletableEntityRepository<Member> memberRepository;

        public InventoryService(
            IDeletableEntityRepository<Record> recordRepository,
            IDeletableEntityRepository<SubCategory> subcategoryRepository,
            IDeletableEntityRepository<Item> itemRepository,
            IDeletableEntityRepository<MeasureUnit> measureUnitRepository,
            IDeletableEntityRepository<Member> memberRepository)
        {
            this.recordRepository = recordRepository;
            this.subcategoryRepository = subcategoryRepository;
            this.itemRepository = itemRepository;
            this.measureUnitRepository = measureUnitRepository;
            this.memberRepository = memberRepository;
        }

        public async Task AddToInventory(AddToInventoryInputModel productView, string userId)
        {
            var product = new Record()
            {
                SubCategoryId = productView.SubCategoryId,
                ItemId = productView.ItemId,
                MemberId = productView.MemberId,
                Quantity = double.Parse(productView.Quantity, CultureInfo.InvariantCulture),
                MeasureUnitId = productView.MeasureUnitId,
                ExpireDate = Convert.ToDateTime(productView.ExpireDate),
                UserId = userId,
            };
            await this.recordRepository.AddAsync(product);
            await this.recordRepository.SaveChangesAsync();
        }

        public async Task Delete(string userId, int id)
        {
            var product = this.recordRepository.AllAsNoTracking()
               .Where(x => x.UserId == userId & x.Id == id)
               .FirstOrDefault();
            product.Quantity = 0;
            this.recordRepository.Update(product);
            await this.recordRepository.SaveChangesAsync();
        }

        public async Task Edit(EditViewModel editModel, string userId, int id)
        {
            var product = this.recordRepository.AllAsNoTracking()
               .Where(x => x.UserId == userId & x.Id == id)
               .FirstOrDefault();
            product.SubCategoryId = editModel.SubCategoryId;
            product.MemberId = editModel.MemberId;
            product.MeasureUnitId = editModel.MeasureUnitId;
            product.ExpireDate = Convert.ToDateTime(editModel.ExpireDate);
            product.Quantity = double.Parse(editModel.Quantity, CultureInfo.InvariantCulture);
            product.ItemId = editModel.ItemId;
            this.recordRepository.Update(product);
            await this.recordRepository.SaveChangesAsync();
        }

        public T GetById<T>(string userId, int id)
        {
            var product = this.recordRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id)
                .To<T>()
                .FirstOrDefault();
            return product;
        }

        public InventoryQueryModel InventoryList(string userId, int itemsPerPage, string searchTerm, int subCategoryId, int pageNumber = 1)
        {
            var productList = this.recordRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.ExpireDate != null & x.Quantity > 0);
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productList = productList.Where(x => x.Item.Name.ToLower().Contains(searchTerm.ToLower()) || x.Member.Name.ToLower().Contains(searchTerm) ||
                x.MeasureUnit.Name.ToLower().Contains(searchTerm));
            }

            if (subCategoryId > 0)
            {
                productList = productList.Where(x => x.SubCategoryId == subCategoryId);
            }

            var inventory = new List<InventoryRecordViewModel>();
            foreach (var product in productList)
            {
                var model = new InventoryRecordViewModel()
                {
                    SubCategoryName = this.subcategoryRepository.AllWithDeleted().Where(x => x.Id == product.SubCategoryId).FirstOrDefault().Name,
                    ItemName = this.itemRepository.AllWithDeleted().Where(x => x.Id == product.ItemId).FirstOrDefault().Name,
                    MeasureUnitShortName = this.measureUnitRepository.AllWithDeleted().FirstOrDefault(x => x.Id == product.MeasureUnitId).ShortName,
                    MemberName = this.memberRepository.AllWithDeleted().FirstOrDefault(x => x.Id == product.MemberId).Name,
                    ExpireDate = (DateTime)product.ExpireDate,
                    Id = product.Id,
                    Quantity = product.Quantity.ToString(),
                };
                inventory.Add(model);
            }

            var products = inventory.OrderBy(x => x.ExpireDate)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage);

            var inventoryModel = new InventoryQueryModel()
            {
                TotalProducts = productList.Count(),
                ItemsPerPage = itemsPerPage,
                PageNumber = pageNumber,
                Products = products,
            };
            return inventoryModel;
        }

        public string ProductName(string userId, int id)
        {
            var productId = this.recordRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id).FirstOrDefault().ItemId;
            var productName = this.itemRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == productId).FirstOrDefault().Name;
            return productName.ToString();
        }
    }
}
