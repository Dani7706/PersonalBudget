namespace PersonalBudget.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class About : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
