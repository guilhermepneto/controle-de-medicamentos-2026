namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public abstract class EntidadeBase<T>
{
    public int Id { get; set; }

    public abstract List<string> Validar();
    public abstract void Atualizar(T entidadeAtualizada);
}
