namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public record ListarPacienteViewModel(
    int Id,
    string Nome,
    string Telefone,
    string CartaoSUS
);

public record CadastrarPacienteViewModel(
    string Nome,
    string Telefone,
    string CartaoSUS,
    string Cpf
);

public record EditarPacienteViewModel(
    int Id,
    string Nome,
    string Telefone,
    string CartaoSUS,
    string Cpf
);

public record ExcluirPacienteViewModel(
    int Id,
    string Nome
);
