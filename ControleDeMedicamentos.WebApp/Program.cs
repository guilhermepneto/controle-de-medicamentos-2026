using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ContextoJson>();
builder.Services.AddScoped<RepositorioMedicamentoEmArquivo>();
builder.Services.AddScoped<RepositorioFornecedorEmArquivo>();

builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

app.UseRouting();
app.MapDefaultControllerRoute();

app.UseStaticFiles();

app.Run();

