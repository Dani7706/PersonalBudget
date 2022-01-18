namespace PersonalBudget.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Data;
    using PersonalBudget.Web.ViewModels.Members;

    using static PersonalBudget.Web.Infrastructure.ClaimsPrincipalExtensions;

    [Authorize]
    public class MembersController : Controller
    {
        private readonly IMemberService memberService;

        public MembersController(IMemberService memberService)
        {
            this.memberService = memberService;
        }

        public IActionResult CreateMember()
        {
            var model = new CreateMemberInputModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string userId = this.User.GetId();

            if (this.memberService.Exists(userId, model.Name) == true)
            {
                this.ModelState.AddModelError(string.Empty, $"{model.Name} вече съществува.");
                return this.View(model);
            }

            await this.memberService.CreateMember(model, userId);
            this.TempData["Message"] = $"{model.Name} бе добавен/а успешно.";
            return this.Redirect("/Members/AllMembers");
        }

        public IActionResult AllMembers()
        {
            string userId = this.User.GetId();
            var model = new AllMembersViewModel
            {
                Members = this.memberService.GetAll<MemberViewModel>(userId),
            };
            return this.View(model);
        }

        public IActionResult Edit(int id)
        {
            string userId = this.User.GetId();
            var member = this.memberService.GetById<MemberViewModel>(userId, id);
            if (member == null)
            {
                return this.BadRequest();
            }

            return this.View(member);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MemberViewModel editMember)
        {
            string userId = this.User.GetId();
            if (!this.ModelState.IsValid)
            {
                return this.View(editMember);
            }

            if (this.memberService.Exists(userId, editMember.Name) == true)
            {
                this.ModelState.AddModelError(string.Empty, $"{editMember.Name} вече съществува.");
                return this.View(editMember);
            }

            await this.memberService.Edit(editMember, userId, id);
            this.TempData["Message"] = $"{editMember.Name} бе успешно редактиран.";
            return this.Redirect("/Members/AllMembers");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = this.User.GetId();
            await this.memberService.Delete(userId, id);
            return this.Redirect("/Members/AllMembers");
        }
    }
}
