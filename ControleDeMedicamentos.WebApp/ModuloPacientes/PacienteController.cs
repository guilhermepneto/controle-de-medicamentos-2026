using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public class PacienteController : Controller
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
        List<ListarPacienteViewModel> viewModels = [];

        foreach (Paciente p in repositorio.SelecionarTodos())
        {
            ListarPacienteViewModel viewModel = new ListarPacienteViewModel(
                p.Id,
                p.Nome,
                p.Telefone,
                p.CartaoSUS
            );

            viewModels.Add(viewModel);
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarPacienteViewModel viewModel = new CadastrarPacienteViewModel(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarPacienteViewModel viewModel)
    {
        Paciente paciente = new Paciente(
            viewModel.Nome,
            viewModel.Telefone,
            viewModel.CartaoSUS,
            viewModel.Cpf
        );

        repositorio.Cadastrar(paciente);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Paciente? paciente = repositorio.SelecionarPorId(id);

        if (paciente == null)
            return NotFound();

        EditarPacienteViewModel viewModel = new EditarPacienteViewModel(
            id,
            paciente.Nome,
            paciente.Telefone,
            paciente.CartaoSUS,
            paciente.Cpf
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarPacienteViewModel viewModel)
    {
        Paciente pacienteAtualizado = new Paciente(
            viewModel.Nome,
            viewModel.Telefone,
            viewModel.CartaoSUS,
            viewModel.Cpf
        );

        bool conseguiuEditar = repositorio.Editar(viewModel.Id, pacienteAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Paciente? paciente = repositorio.SelecionarPorId(id);

        if (paciente == null)
            return NotFound();

        ExcluirPacienteViewModel viewModel = new ExcluirPacienteViewModel(
            id,
            paciente.Nome
        );

        return View(viewModel);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(int id)
    {
        bool conseguiuExcluir = repositorio.Excluir(id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
