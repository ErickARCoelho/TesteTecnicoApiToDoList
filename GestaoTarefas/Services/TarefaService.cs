using GestaoTarefas.Domain;
using GestaoTarefas.DTOs.Tarefas;
using GestaoTarefas.Repositories;

namespace GestaoTarefas.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repository;

        public TarefaService(ITarefaRepository repository) => _repository = repository;

        public async Task<TarefaReadDto> CreateTarefaAsync(TarefaCreateDto tarefaDto)
        {
            var tarefa = new Tarefa
            {
                Titulo = tarefaDto.Titulo,
                Descricao = tarefaDto.Descricao,
                DataDeVencimento = tarefaDto.DataDeVencimento,
                Status = tarefaDto.Status
            };

            var createdTarefa = await _repository.CreateTarefaAsync(tarefa);

            return new TarefaReadDto
            {
                Id = createdTarefa.Id,
                Titulo = createdTarefa.Titulo,
                Descricao = createdTarefa.Descricao,
                DataDeVencimento = createdTarefa.DataDeVencimento,
                Status = createdTarefa.Status
            };
        }

        public async Task<TarefaReadDto> GetTarefaByIdAsync(int id)
        {
            var tarefa = await _repository.GetTarefaByIdAsync(id);
            if (tarefa == null)
                return null;

            return new TarefaReadDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                DataDeVencimento = tarefa.DataDeVencimento,
                Status = tarefa.Status
            };
        }

        public async Task<IEnumerable<TarefaReadDto>> GetTarefasAsync()
        {
            var tarefas = await _repository.GetTarefasAsync();
            return tarefas.Select(t => new TarefaReadDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                DataDeVencimento = t.DataDeVencimento,
                Status = t.Status
            });
        }

        public async Task<IEnumerable<TarefaReadDto>> GetTarefasByFilterAsync(TarefaStatus? status, DateTime? dataDeVencimento)
        {
            var tarefas = await _repository.GetTarefasByFilterAsync(status, dataDeVencimento);
            return tarefas.Select(t => new TarefaReadDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                DataDeVencimento = t.DataDeVencimento,
                Status = t.Status
            });
        }

        public async Task<bool> UpdateTarefaAsync(int id, TarefaUpdateDto tarefaDto)
        {
            var tarefaExistente = await _repository.GetTarefaByIdAsync(id);
            if (tarefaExistente == null)
                return false;

            tarefaExistente.Titulo = tarefaDto.Titulo;
            tarefaExistente.Descricao = tarefaDto.Descricao;
            tarefaExistente.DataDeVencimento = tarefaDto.DataDeVencimento;
            tarefaExistente.Status = tarefaDto.Status;

            await _repository.UpdateTarefaAsync(tarefaExistente);
            return true;
        }

        public async Task<bool> DeleteTarefaAsync(int id)
        {
            var tarefaExistente = await _repository.GetTarefaByIdAsync(id);
            if (tarefaExistente == null)
                return false;

            await _repository.DeleteTarefaAsync(tarefaExistente);
            return true;
        }
    }
}
