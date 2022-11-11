using FluentValidation;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using PhoneStore;
using PhoneStore.Models;
using PhoneStore.Services;
using PhoneStore.Validation;
using System;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc().AddViewLocalization();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory() + "/Logs/", "logger.txt"));
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddScoped<IValidator<User>, UserValidator>();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("ru"),
        new CultureInfo("uk")
    };

    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
var app = builder.Build();

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files")),

    RequestPath = new PathString("/pages")
});
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions() 
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files")),
    RequestPath = new PathString("/pages")
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
