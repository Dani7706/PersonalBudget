namespace PersonalBudget.Web.Areas.Administration.Controllers
{
    using PersonalBudget.Common;
    using PersonalBudget.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
