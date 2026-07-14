using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes;

public class RequisicaoSaida : EntidadeBase
{
    public Paciente Paciente { get; set; } = null!;
    public Medicamento Medicamento { get; set; } = null!;
    public int Quantidade { get; set; }
    public DateTime Data { get; set; } = DateTime.Now;


    public RequisicaoSaida() { }

    public RequisicaoSaida(Medicamento medicamento, int quantidade, Paciente paciente) : this()
    {
        Medicamento = medicamento;
        Quantidade = quantidade;
        Paciente = paciente;

        medicamento.RegistrarRequisicaoSaida(this);
    }
    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Paciente == null)
            erros.Add("O campo \"Paciente\" deve ser preenchido.");

        if (Medicamento == null)
            erros.Add("O campo \"Medicamento\" deve ser preenchido.");

        if (Quantidade <= 0)
            erros.Add("A \"Quantidade\" deve ser maior que zero.");

        if (Medicamento != null && Quantidade > Medicamento.QuantidadeEmEstoque)
            erros.Add("A quantidade solicitada excede o estoque disponível.");

        return erros;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        RequisicaoSaida requisicaoAtualizada = (RequisicaoSaida)entidadeAtualizada;

        Medicamento = requisicaoAtualizada.Medicamento;
        Quantidade = requisicaoAtualizada.Quantidade;
        Paciente = requisicaoAtualizada.Paciente;
    }
}
