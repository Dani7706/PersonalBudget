namespace PersonalBudget.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.ViewModels.Records;
    using PersonalBudget.Web.ViewModels.TransferViewModels;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Authorize]
    public class TransfersController : Controller
    {
        private readonly IPartnerService partnerService;
        private readonly ITransferService transferService;
        private readonly IDeletableEntityRepository<Transfer> transferRepository;
        private readonly IProductService productService;
        private readonly IMemberService memberService;
        private readonly ISubcategoryService subcategoryService;
        private readonly IMeasureUnitsService measureUnitsService;
        private readonly IRecordsService recordsService;

        public TransfersController(
            IPartnerService partnerService,
            ITransferService transferService,
            IDeletableEntityRepository<Transfer> transferRepository,
            IProductService productService,
            IMemberService memberService,
            ISubcategoryService subcategoryService,
            IMeasureUnitsService measureUnitsService,
            IRecordsService recordsService)
        {
            this.partnerService = partnerService;
            this.transferService = transferService;
            this.transferRepository = transferRepository;
            this.productService = productService;
            this.memberService = memberService;
            this.subcategoryService = subcategoryService;
            this.measureUnitsService = measureUnitsService;
            this.recordsService = recordsService;
        }

        public IActionResult Create()
        {
            string userId = this.User.GetId();
            var viewModel = new CreateTransferInputModel
            {
                TransferNumber = this.transferRepository.All().Where(x => x.UserId == userId).Count() + 1,
                Partners = this.partnerService.GetAllPartners(userId),
                Items = this.productService.GetAllProducts(userId),
                Members = this.memberService.GetAllMembers(userId),
                Subcategories = this.subcategoryService.GetAllSubCategories(userId),
                MeasureUnits = this.measureUnitsService.GetAllMeasureUnits(),
                Records = new List<CreateRecordInputModel>(new[]
            {
                new CreateRecordInputModel
                {
                    Items = this.productService.GetAllProducts(userId),
                    MeasureUnits = this.measureUnitsService.GetAllMeasureUnits(),
                    Members = this.memberService.GetAllMembers(userId),
                    Subcategories = this.subcategoryService.GetAllSubCategories(userId),
                },
            }),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransferInputModel transfer)
        {
            string userId = this.User.GetId();
            var errors = this.ModelState.Values.SelectMany(v => v.Errors);
            if (!this.ModelState.IsValid)
            {
                transfer.TransferNumber = this.transferRepository.All().Where(x => x.UserId == userId).Count() + 1;
                transfer.Partners = this.partnerService.GetAllPartners(userId);
                transfer.Items = this.productService.GetAllProducts(userId);
                transfer.Members = this.memberService.GetAllMembers(userId);
                transfer.Subcategories = this.subcategoryService.GetAllSubCategories(userId);
                transfer.MeasureUnits = this.measureUnitsService.GetAllMeasureUnits();
                foreach (var error in errors)
                {
                    throw new System.Exception(error.ErrorMessage);
                }

                return this.View(transfer);
            }

            await this.transferService.CreateTransfer(transfer, userId);
            var lastTransfer = this.transferRepository.AllAsNoTracking().OrderBy(x => x.CreatedOn).LastOrDefault();
            var transferId = lastTransfer.Id;
            this.TempData["Message"] = "Трансферът бе добавен успешно.";


            return this.RedirectToAction("ById", "Transfers", new { id = transferId });
        }

        public IActionResult All([FromQuery] AllTransfersViewModel allTransfers)
        {
            string userId = this.User.GetId();
            int itemsPerPage = allTransfers.ItemsPerPage;
            if (itemsPerPage == 0)
            {
                itemsPerPage = 6;
            }

            var viewModel = new AllTransfersViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = allTransfers.PageNumber,
                Count = this.transferService.GetCount(userId, allTransfers.SearchTerm, allTransfers.PartnerId),
                Partners = this.partnerService.GetAllPartners(userId),
                Transfers = this.transferService.GetAll<TransferViewModel>(allTransfers.PageNumber, userId, allTransfers.SearchTerm, allTransfers.PartnerId, itemsPerPage),
            };
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            string userId = this.User.GetId();
            var transfer = this.transferService.GetById<SingleTransferViewModel>(userId, id);
            return this.View(transfer);
        }

        public IActionResult Edit(int id)
        {
            string userId = this.User.GetId();
            var transfer = this.transferService.GetById<EditTransferViewModel>(userId, id);
            transfer.Partners = this.partnerService.GetAllPartners(userId);
            foreach (var record in transfer.Records)
            {
                record.Items = this.productService.GetAllProducts(userId);
                record.Members = this.memberService.GetAllMembers(userId);
                record.Subcategories = this.subcategoryService.GetAllSubCategories(userId);
                record.MeasureUnits = this.measureUnitsService.GetAllMeasureUnits();
            }

            return this.View(transfer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTransferViewModel editTransfer)
        {
            string userId = this.User.GetId();
            var errors = this.ModelState.Values.SelectMany(v => v.Errors);
            if (!this.ModelState.IsValid)
            {
                editTransfer.Partners = this.partnerService.GetAllPartners(userId);
                foreach (var record in editTransfer.Records)
                {
                    record.Items = this.productService.GetAllProducts(userId);
                    record.Members = this.memberService.GetAllMembers(userId);
                    record.Subcategories = this.subcategoryService.GetAllSubCategories(userId);
                    record.MeasureUnits = this.measureUnitsService.GetAllMeasureUnits();
                }

                return this.View(editTransfer);
            }

            await this.transferService.Edit(userId, id, editTransfer);
            this.TempData["Message"] = "Трансферът бе редактиран успешно.";

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        public IActionResult Details(int id)
        {
            string userId = this.User.GetId();
            var transfer = this.transferService.GetById<Details>(userId, id);
            return this.View(transfer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            string userId = this.User.GetId();
            await this.transferService.Delete(userId, id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
