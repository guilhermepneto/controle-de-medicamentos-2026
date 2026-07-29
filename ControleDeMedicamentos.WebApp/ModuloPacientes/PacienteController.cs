using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public sealed class PacienteController : Controller
{
    private readonly RepositorioPacienteEmArquivo repositorioPaciente;

    public PacienteController()
    {
        ContextoJson contextoJson = new ContextoJson();

        contextoJson.Carregar();

        repositorioPaciente = new RepositorioPacienteEmArquivo(contextoJson);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Paciente> pacientes = repositorioPaciente.SelecionarTodos();

        List<ListarPacienteViewModel> viewModels = new List<ListarPacienteViewModel>();

        foreach (Paciente p in pacientes)
        {
            ListarPacienteViewModel vm = new ListarPacienteViewModel(
                p.Id,
                p.Nome,
                p.Telefone,
                p.CartaoSus
            );

            viewModels.Add(vm);
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarPacienteViewModel cadastrarVm)
    {
        Paciente paciente = new Paciente(
            cadastrarVm.Nome,
            cadastrarVm.Telefone,
            cadastrarVm.CartaoSus,
            cadastrarVm.Cpf
            );

        repositorioPaciente.Cadastrar(paciente);

        return RedirectToAction(nameof(Listar));

    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        if (paciente == null)
            return NotFound();

        EditarPacienteViewModel viewModel = new EditarPacienteViewModel(
          id,
          paciente.Nome,
          paciente.Telefone,
          paciente.CartaoSus,
          paciente.Cpf
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarPacienteViewModel editarVm)
    {
        Paciente pacienteAtualizado = new Paciente(
            editarVm.Nome,
            editarVm.Telefone,
            editarVm.CartaoSus,
            editarVm.Cpf
            );

        bool conseguiuEditar = repositorioPaciente.Editar(editarVm.Id, pacienteAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        if (paciente == null)
            return NotFound();

        ExcluirPacienteViewModel viewModel = new ExcluirPacienteViewModel(
            id,
            paciente.Nome
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirPacienteViewModel excluirVm)
    {
        bool conseguiuExcluir = repositorioPaciente.Excluir(excluirVm.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

}
