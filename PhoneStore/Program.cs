using PhoneStore;
using PhoneStore.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory() + "/Logs/", "logger.txt"));
var app = builder.Build();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
