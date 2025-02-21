using GestaoTarefas.Domain;

namespace GestaoTarefas.Repositories
{
    public interface ITarefaRepository
    {
        Task<Tarefa> CreateTarefaAsync(Tarefa tarefa);
        Task<Tarefa> GetTarefaByIdAsync(int id);
        Task<IEnumerable<Tarefa>> GetTarefasAsync();
        Task<IEnumerable<Tarefa>> GetTarefasByFilterAsync(TarefaStatus? status, DateTime? dataVencimento);
        Task UpdateTarefaAsync(Tarefa tarefa);
        Task DeleteTarefaAsync(Tarefa tarefa);
        Task<bool> SaveChangesAsync();
    }
}
