using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;
using PhoneStore.Services;
using System;
using System.Text;
using FluentValidation.AspNetCore;

namespace WebServicesProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private IValidator<User> _validator;
        public HomeController(IEmailSender emailSender, ILogger<HomeController> logger, IHttpContextAccessor contextAccessor, IValidator<User> validator)
        {
            _contextAccessor = contextAccessor;
            _logger = logger;
            _emailSender = emailSender;
            _validator = validator;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"time - {DateTime.Now.ToShortTimeString()};path -  {HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}; remoteIP -  {_contextAccessor?.HttpContext?.Connection.RemoteIpAddress}");

            return View();
        }

        public IActionResult About()
        {
            _logger.LogInformation($"time - {DateTime.Now.ToShortTimeString()};path -  {HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}; remoteIP -  {_contextAccessor?.HttpContext?.Connection.RemoteIpAddress}");

            return View();
        }

        public IActionResult Brand()
        {
            _logger.LogInformation($"time - {DateTime.Now.ToShortTimeString()};path -  {HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}; remoteIP -  {_contextAccessor?.HttpContext?.Connection.RemoteIpAddress}");

            return View();
        }

        public IActionResult Specials()
        {
            _logger.LogInformation($"time - {DateTime.Now.ToShortTimeString()};path -  {HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}; remoteIP -  {_contextAccessor?.HttpContext?.Connection.RemoteIpAddress}");

            return View();
        }

        public IActionResult Contact()
        {
            _logger.LogInformation($"time - {DateTime.Now.ToShortTimeString()};path -  {HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}; remoteIP -  {_contextAccessor?.HttpContext?.Connection.RemoteIpAddress}");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(MailData mailData)
        {
            _logger.LogInformation($"time - {DateTime.Now.ToShortTimeString()};path -  {HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}; remoteIP -  {_contextAccessor?.HttpContext?.Connection.RemoteIpAddress}");

            string message = $"Hi, its {mailData.Name}!\n{mailData.Message}\nMy phone is: {mailData.Phone}, email: {mailData.Email}";
            await _emailSender.SendEmailAsync("2003harik20032@gmail.com", "LAB2", message);
            return RedirectToAction("Index");
        }
        public IActionResult FileLoad()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FileLoad(List<IFormFile> files)
        {
            var size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Files", formFile.FileName);
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true }
            );

            return LocalRedirect(returnUrl);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            ValidationResult result = await _validator.ValidateAsync(user);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);


                return View("Create", user);
            }

            return RedirectToAction("Index");

        }
    }
}
