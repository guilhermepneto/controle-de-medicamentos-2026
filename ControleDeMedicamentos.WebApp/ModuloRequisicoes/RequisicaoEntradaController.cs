using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class RequisicaoEntradaController : Controller
{
    private readonly RepositorioRequisicaoEntradaEmArquivo repositorio;
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
    private readonly RepositorioFuncionarioEmArquivo repositorioFuncionario;

    public RequisicaoEntradaController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorio = new RepositorioRequisicaoEntradaEmArquivo(contexto);
        repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
        repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarRequisicaoEntradaViewModel> viewModels = [];

        foreach (RequisicaoEntrada requisicao in repositorio.SelecionarTodos())
        {
            ListarRequisicaoEntradaViewModel viewModel = new ListarRequisicaoEntradaViewModel(
                requisicao.Id,
                requisicao.Medicamento.Nome,
                requisicao.Funcionario.Nome,
                requisicao.Quantidade,
                requisicao.Data
            );

            viewModels.Add(viewModel);
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarRequisicaoEntradaViewModel viewModel = new CadastrarRequisicaoEntradaViewModel(
            0,
            0,
            0
        ) with
        { Medicamentos = ObterMedicamentos(), Funcionarios = ObterFuncionarios() };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarRequisicaoEntradaViewModel viewModel)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(viewModel.MedicamentoId);

        if (medicamento == null)
            return NotFound();

        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(viewModel.FuncionarioId);

        if (funcionario == null)
            return NotFound();

        RequisicaoEntrada requisicaoEntrada = new RequisicaoEntrada(
            medicamento,
            viewModel.Quantidade,
            funcionario
        );

        repositorio.Cadastrar(requisicaoEntrada);

        return RedirectToAction(nameof(Listar));
    }

    private List<MedicamentoRequisicaoEntradaViewModel> ObterMedicamentos()
    {
        List<MedicamentoRequisicaoEntradaViewModel> viewModels = [];

        foreach (Medicamento medicamento in repositorioMedicamento.SelecionarTodos())
        {
            MedicamentoRequisicaoEntradaViewModel viewModel = new MedicamentoRequisicaoEntradaViewModel(
                medicamento.Id,
                medicamento.Nome
            );

            viewModels.Add(viewModel);
        }

        return viewModels;
    }

    private List<FuncionarioRequisicaoEntradaViewModel> ObterFuncionarios()
    {
        List<FuncionarioRequisicaoEntradaViewModel> viewModels = [];

        foreach (Funcionario funcionario in repositorioFuncionario.SelecionarTodos())
        {
            FuncionarioRequisicaoEntradaViewModel viewModel = new FuncionarioRequisicaoEntradaViewModel(
                funcionario.Id,
                funcionario.Nome
            );

            viewModels.Add(viewModel);
        }

        return viewModels;
    }
}
