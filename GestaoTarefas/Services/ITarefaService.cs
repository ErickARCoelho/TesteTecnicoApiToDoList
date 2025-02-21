using GestaoTarefas.Domain;
using GestaoTarefas.DTOs.Tarefas;

namespace GestaoTarefas.Services
{
    public interface ITarefaService
    {
        Task<TarefaReadDto> CreateTarefaAsync(TarefaCreateDto tarefaDto);
        Task<TarefaReadDto> GetTarefaByIdAsync(int id);
        Task<IEnumerable<TarefaReadDto>> GetTarefasAsync();
        Task<IEnumerable<TarefaReadDto>> GetTarefasByFilterAsync(TarefaStatus? status, DateTime? dataDeVencimento);
        Task<bool> UpdateTarefaAsync(int id, TarefaUpdateDto tarefaDto);
        Task<bool> DeleteTarefaAsync(int id);
    }
}
