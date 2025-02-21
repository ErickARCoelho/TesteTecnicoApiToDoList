using GestaoTarefas.Domain;

namespace GestaoTarefas.DTOs.Tarefas
{
    public class TarefaReadDto
    {
        public int Id { get; set; }

        public string? Titulo { get; set; }

        public string? Descricao { get; set; }

        public DateTime? DataDeVencimento { get; set; }

        public TarefaStatus Status { get; set; }
    }
}
