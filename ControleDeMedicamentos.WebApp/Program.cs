WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

app.UseRouting();
app.MapDefaultControllerRoute();

app.UseStaticFiles();

app.Run();

