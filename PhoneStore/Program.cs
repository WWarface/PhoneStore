using Microsoft.Extensions.FileProviders;
using PhoneStore;
using PhoneStore.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory() + "/Logs/", "logger.txt"));
var app = builder.Build();

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
