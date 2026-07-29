using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionarios;

public sealed class FuncionarioController : Controller
{
    private readonly RepositorioFuncionarioEmArquivo repositorioFuncionario;

    public FuncionarioController()
    {
        ContextoJson contextoJson = new ContextoJson();

        contextoJson.Carregar();

        repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextoJson);

    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();

        List<ListarFuncionarioViewModel> viewModels = new List<ListarFuncionarioViewModel>();

        foreach (Funcionario f in funcionarios)
        {
            ListarFuncionarioViewModel vm = new ListarFuncionarioViewModel(
                f.Id,
                f.Nome,
                f.Telefone
            );

            viewModels.Add(vm);
        }

        return View(viewModels);
    }

    public ActionResult Cadastrar()
    {
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();

        return View(funcionarios);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarFuncionarioViewModel cadastrarVm)
    {
        Funcionario funcionario = new Funcionario(
            cadastrarVm.Nome,
            cadastrarVm.Telefone,
            cadastrarVm.Cpf
            );

        repositorioFuncionario.Cadastrar(funcionario);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        if (funcionario == null)
            return NotFound();

        EditarFuncionarioViewModel viewModel = new EditarFuncionarioViewModel(
          id,
          funcionario.Nome,
          funcionario.Telefone,
          funcionario.Cpf
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarFuncionarioViewModel editarVm)
    {

        Funcionario funcionarioAtualizado = new Funcionario(
            editarVm.Nome,
            editarVm.Telefone,
            editarVm.Cpf
            );

        bool conseguiuEditar = repositorioFuncionario.Editar(editarVm.Id, funcionarioAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        if (funcionario == null)
            return NotFound();

        return View(funcionario);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirFuncionarioViewModel excluirVm)
    {
        bool conseguiuExcluir = repositorioFuncionario.Excluir(excluirVm.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

}
