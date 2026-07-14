using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class TelaPaciente : TelaBase<Paciente>, ITelaOpcoes, ITelaCrud
{
    public TelaPaciente(RepositorioPacienteEmArquivo repositorio) : base("Paciente", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Pacientes");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0,-5} | {1,-30} | {2,-15} | {3,-15} | {4,-11}",
            "Id", "Nome", "Telefone", "Cartão SUS", "CPF"
        );

        List<Paciente> registros = repositorio.SelecionarTodos();

        foreach (Paciente p in registros)
        {
            Console.WriteLine(
               "{0,-5} | {1,-30} | {2,-15} | {3,-15} | {4,-11}",
               p.Id,
               p.Nome,
               p.Telefone,
               p.CartaoSus,
               p.Cpf
           );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Pressione ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Paciente ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do paciente: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone do paciente: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o número do cartão do SUS: ");
        string cartaoSus = Console.ReadLine() ?? string.Empty;

        Console.Write("Informe o CPF do paciente: ");
        string Cpf = Console.ReadLine() ?? string.Empty;

        return new Paciente(nome, telefone, cartaoSus, Cpf);
    }

    protected override bool ExisteRegistroComInformacoesExclusivas(Paciente entidade, int? idIgnorado = null)
    {
        List<Paciente> registros = repositorio.SelecionarTodos();

        foreach (Paciente paciente in registros)
        {
            if (paciente.Id != idIgnorado && paciente.CartaoSus == entidade.CartaoSus)
            {
                Console.WriteLine("---------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já existe um paciente cadastrado com este Cartão SUS.");
                Console.ResetColor();
                Console.WriteLine("---------------------------------");

                return true;
            }
        }

        return false;
    }
}
