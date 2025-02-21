using GestaoTarefas.Domain;
using System.ComponentModel.DataAnnotations;

namespace GestaoTarefas.DTOs.Tarefas
{
    public class TarefaCreateDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        public string? Titulo { get; set; }

        public string? Descricao { get; set; }

        public DateTime? DataDeVencimento { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        public TarefaStatus Status { get; set; }
    }
}
