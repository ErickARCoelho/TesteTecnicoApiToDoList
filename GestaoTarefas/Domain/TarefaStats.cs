using System.ComponentModel.DataAnnotations;

namespace GestaoTarefas.Domain
{
    public enum TarefaStatus
    {
        [Display(Name = "Pendente")]
        Pendente,
        [Display(Name = "Em progresso")]
        EmProgresso,
        [Display(Name = "Concluída")]
        Concluida
    }
}
