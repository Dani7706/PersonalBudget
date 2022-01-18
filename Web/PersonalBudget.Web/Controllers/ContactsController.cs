namespace PersonalBudget.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PersonalBudget.Services.Messaging;
    using PersonalBudget.Web.ViewModels;

    public class ContactsController : BaseController
    {
        private const string RedirectedFromContactForm = "RedirectedFromContactForm";

        private readonly IEmailSender emailSender;

        public ContactsController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormViewModel contactFormView)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(contactFormView);
            }
            await this.emailSender.SendEmailAsync(
                contactFormView.Email,
                contactFormView.Name,
                "peneva1977@abv.bg",
                contactFormView.Title,
                contactFormView.Content);
            this.TempData[RedirectedFromContactForm] = true;
            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            if (this.TempData[RedirectedFromContactForm] == null)
            {
                return this.NotFound();
            }
            return this.View();
        }
    }
}
