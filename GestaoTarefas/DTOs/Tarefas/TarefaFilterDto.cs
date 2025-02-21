using GestaoTarefas.Domain;
using System.ComponentModel.DataAnnotations;

namespace GestaoTarefas.DTOs.Tarefas
{
    public class TarefaFilterDto
    {
        [Display(Name = "Status da Tarefa")]
        public TarefaStatus? Status { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Vencimento")]
        public DateTime? DataDeVencimento { get; set; }
    }
}
