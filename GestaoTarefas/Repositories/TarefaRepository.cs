using GestaoTarefas.Data;
using GestaoTarefas.Domain;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _context;

        public TarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tarefa> CreateTarefaAsync(Tarefa tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
            await SaveChangesAsync();
            return tarefa;
        }

        public async Task<Tarefa> GetTarefaByIdAsync(int id) => await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);

        public async Task<IEnumerable<Tarefa>> GetTarefasAsync()
        {
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasByFilterAsync(TarefaStatus? status, DateTime? dataVencimento)
        {
            var query = _context.Tarefas.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status);
            }

            if (dataVencimento.HasValue)
            {
                query = query.Where(t => t.DataDeVencimento.HasValue &&
                                         t.DataDeVencimento.Value.Date == dataVencimento.Value.Date);
            }

            return await query.ToListAsync();
        }

        public async Task UpdateTarefaAsync(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            await SaveChangesAsync();
        }

        public async Task DeleteTarefaAsync(Tarefa tarefa)
        {
            _context.Tarefas.Remove(tarefa);
            await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
