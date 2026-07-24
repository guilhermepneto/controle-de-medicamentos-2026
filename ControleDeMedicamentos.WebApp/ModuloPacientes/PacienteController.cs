using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public sealed class PacienteController : Controller
{
    private readonly RepositorioPacienteEmArquivo repositorio;

    public PacienteController()
    {
        ContextoJson contexto = new ContextoJson();

        contexto.Carregar();

        repositorio = new RepositorioPacienteEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Paciente> pacientes = repositorio.SelecionarTodos();

        return View(pacientes);
    }
}
