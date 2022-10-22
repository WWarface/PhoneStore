using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;
using PhoneStore.Services;
using System.Text;

namespace WebServicesProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;

        public HomeController(IEmailSender emailSender)
        {
            _emailSender=emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Brand()
        {
            return View();
        }

        public IActionResult Specials()
        {
            return View();
        }

        public IActionResult Contact()
        {      
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(MailData mailData)
        {
            string message = $"Hi, its {mailData.Name}!\n{mailData.Message}\nMy phone is: {mailData.Phone}, email: {mailData.Email}";
            await _emailSender.SendEmailAsync("matyiokin2002@gmail.com", "LAB2", message);
            return RedirectToAction("Index");
        }
    }
}
