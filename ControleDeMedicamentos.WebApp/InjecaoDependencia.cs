using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;
using ControleDeMedicamentos.WebApp.ModuloRequisicoes;

namespace ControleDeMedicamentos.WebApp;

public static class InjecaoDependencia
{
    public static void AddInfraestruturaEmJson(this IServiceCollection services)
    {
        services.AddScoped(_ =>
{
    ContextoJson contexto = new ContextoJson();
    contexto.Carregar();

    return contexto;
});

        services.AddScoped<RepositorioMedicamentoEmArquivo>();
        services.AddScoped<RepositorioFornecedorEmArquivo>();
        services.AddScoped<RepositorioFuncionarioEmArquivo>();
        services.AddScoped<RepositorioPacienteEmArquivo>();
        services.AddScoped<RepositorioRequisicaoEntradaEmArquivo>();
        services.AddScoped<RepositorioRequisicaoSaidaEmArquivo>();
    }
}
