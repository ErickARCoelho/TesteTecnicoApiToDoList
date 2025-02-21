using GestaoTarefas.DTOs.Tarefas;
using GestaoTarefas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaService _service;

        public TarefasController(ITarefaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Cria uma nova tarefa.
        /// Exemplo de payload:
        /// {
        ///   "titulo": "Reunião com cliente",
        ///   "descricao": "Discutir os requisitos do projeto",
        ///   "dataDeVencimento": "2025-08-15",
        ///   "status": "0"
        /// }
        /// </summary>
        /// <param name="tarefaDto">Dados da tarefa para criação.</param>
        /// <returns>Dados da tarefa criada.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTarefa([FromBody] TarefaCreateDto tarefaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTarefa = await _service.CreateTarefaAsync(tarefaDto);
            return CreatedAtAction(nameof(GetTarefaById), new { id = createdTarefa.Id }, createdTarefa);
        }

        /// <summary>
        /// Retorna uma tarefa pelo seu ID.
        /// Exemplo de chamada: GET /api/tarefas/1
        /// </summary>
        /// <param name="id">ID da tarefa.</param>
        /// <returns>Tarefa encontrada ou 404 se não encontrada.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarefaById(int id)
        {
            var tarefa = await _service.GetTarefaByIdAsync(id);
            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        /// <summary>
        /// Lista tarefas, com possibilidade de filtragem por status e data de vencimento.
        /// Exemplo de chamada:
        /// GET /api/tarefas?Status=1&DataDeVencimento=1986-08-02
        /// O parâmetro "DataDeVencimento" deve estar no formato ISO (YYYY-MM-DD).
        /// </summary>
        /// <param name="filter">Objeto de filtro com os parâmetros de busca.</param>
        /// <returns>Lista de tarefas filtradas.</returns>
        [HttpGet]
        public async Task<IActionResult> GetTarefas([FromQuery] TarefaFilterDto filter)
        {
            var tarefas = await _service.GetTarefasByFilterAsync(filter.Status, filter.DataDeVencimento);
            return Ok(tarefas);
        }

        /// <summary>
        /// Atualiza os dados de uma tarefa existente.
        /// Exemplo de payload:
        /// {
        ///   "titulo": "Reunião com cliente atualizada",
        ///   "descricao": "Novo horário agendado",
        ///   "dataDeVencimento": "2025-08-20",
        ///   "status": "1"
        /// }
        /// </summary>
        /// <param name="id">ID da tarefa a ser atualizada.</param>
        /// <param name="tarefaDto">Dados atualizados da tarefa.</param>
        /// <returns>Status 204 se a atualização for bem-sucedida, ou 404 se não encontrar a tarefa.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarefa(int id, [FromBody] TarefaUpdateDto tarefaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateTarefaAsync(id, tarefaDto);
            if (!result)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Exclui uma tarefa.
        /// Exemplo de chamada: DELETE /api/tarefas/1
        /// </summary>
        /// <param name="id">ID da tarefa a ser excluída.</param>
        /// <returns>Status 204 se a exclusão for bem-sucedida, ou 404 se não encontrar a tarefa.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var result = await _service.DeleteTarefaAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Endpoint de teste que dispara uma exceção intencionalmente para verificar o middleware de tratamento de erros.
        /// Exemplo de chamada: GET /api/tarefas/testexception
        /// </summary>
        [HttpGet("testexception")]
        public IActionResult TestException()
        {
            throw new Exception("Erro intencional para testar o middleware.");
        }
    }
}
