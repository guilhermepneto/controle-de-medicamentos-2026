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

        return View(funcionarios);
    }

    public ActionResult Cadastrar()
    {
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();

        return View(funcionarios);
    }

    [HttpPost]
    public ActionResult Cadastrar(string nome, string telefone, string cpf)
    {
        Funcionario funcionario = new Funcionario(nome, telefone, cpf);

        repositorioFuncionario.Cadastrar(funcionario);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        if (funcionario == null)
            return NotFound();

        return View(funcionario);
    }

    [HttpPost]
    public ActionResult Editar(int id, string nome, string telefone, string cpf)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        Funcionario funcionarioAtualizado = new Funcionario(nome, telefone, cpf);

        bool conseguiuEditar = repositorioFuncionario.Editar(id, funcionarioAtualizado);

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
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(int id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        bool conseguiuExcluir = repositorioFuncionario.Excluir(id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

}
