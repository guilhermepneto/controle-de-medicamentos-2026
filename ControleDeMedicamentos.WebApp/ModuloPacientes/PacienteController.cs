using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public sealed class PacienteController : Controller
{
    private readonly RepositorioPacienteEmArquivo repositorioPaciente;

    public PacienteController()
    {
        ContextoJson contexto = new ContextoJson();

        contexto.Carregar();

        repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Paciente> pacientes = repositorioPaciente.SelecionarTodos();

        return View(pacientes);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(string nome, string telefone, string cartaoSus, string cpf)
    {
        Paciente paciente = new Paciente(nome, telefone, cartaoSus, cpf);

        repositorioPaciente.Cadastrar(paciente);

        return RedirectToAction(nameof(Listar));

    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        if (paciente == null)
            return NotFound();

        return View(paciente);
    }

    [HttpPost]
    public ActionResult Editar(int id, string nome, string telefone, string cartaoSus, string cpf)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        Paciente pacienteAtualizado = new Paciente(nome, telefone, cartaoSus, cpf);

        bool conseguiuEditar = repositorioPaciente.Editar(id, pacienteAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

}
