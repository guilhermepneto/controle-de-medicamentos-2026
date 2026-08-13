using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;
using ControleDeMedicamentos.WebApp.ModuloRequisicoes;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(_ =>
{
    ContextoJson contexto = new ContextoJson();
    contexto.Carregar();

    return contexto;
});

builder.Services.AddScoped<RepositorioMedicamentoEmArquivo>();
builder.Services.AddScoped<RepositorioFornecedorEmArquivo>();
builder.Services.AddScoped<RepositorioFuncionarioEmArquivo>();
builder.Services.AddScoped<RepositorioPacienteEmArquivo>();
builder.Services.AddScoped<RepositorioRequisicaoEntradaEmArquivo>();
builder.Services.AddScoped<RepositorioRequisicaoSaidaEmArquivo>();

builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

app.UseRouting();
app.MapDefaultControllerRoute();

app.UseStaticFiles();

app.Run();

