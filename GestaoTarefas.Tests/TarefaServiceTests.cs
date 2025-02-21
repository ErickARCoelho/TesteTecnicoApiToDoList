using GestaoTarefas.Domain;
using GestaoTarefas.DTOs.Tarefas;
using GestaoTarefas.Repositories;
using GestaoTarefas.Services;
using Moq;

namespace GestaoTarefas.Tests
{
    public class TarefaServiceTests
    {
        private readonly Mock<ITarefaRepository> _repositoryMock;
        private readonly ITarefaService _service;

        public TarefaServiceTests()
        {
            _repositoryMock = new Mock<ITarefaRepository>();
            _service = new TarefaService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CreateTarefaAsync_ValidData_ReturnsTarefaReadDto()
        {
            var tarefaCreateDto = new TarefaCreateDto
            {
                Titulo = "Teste",
                Descricao = "Teste Descrição",
                DataDeVencimento = DateTime.Parse("2025-08-15"),
                Status = TarefaStatus.Pendente
            };

            var tarefa = new Tarefa
            {
                Id = 1,
                Titulo = tarefaCreateDto.Titulo,
                Descricao = tarefaCreateDto.Descricao,
                DataDeVencimento = tarefaCreateDto.DataDeVencimento,
                Status = tarefaCreateDto.Status
            };

            _repositoryMock.Setup(r => r.CreateTarefaAsync(It.IsAny<Tarefa>()))
                .ReturnsAsync(tarefa);

            var result = await _service.CreateTarefaAsync(tarefaCreateDto);

            Assert.NotNull(result);
            Assert.Equal(tarefa.Id, result.Id);
            Assert.Equal(tarefa.Titulo, result.Titulo);
        }

        [Fact]
        public async Task GetTarefaByIdAsync_TarefaExists_ReturnsTarefaReadDto()
        {
            var tarefa = new Tarefa
            {
                Id = 1,
                Titulo = "Teste",
                Descricao = "Teste Descrição",
                DataDeVencimento = DateTime.Parse("2025-08-15"),
                Status = TarefaStatus.Pendente
            };

            _repositoryMock.Setup(r => r.GetTarefaByIdAsync(tarefa.Id))
                .ReturnsAsync(tarefa);

            var result = await _service.GetTarefaByIdAsync(tarefa.Id);

            Assert.NotNull(result);
            Assert.Equal(tarefa.Id, result.Id);
            Assert.Equal(tarefa.Titulo, result.Titulo);
        }

        [Fact]
        public async Task GetTarefaByIdAsync_TarefaDoesNotExist_ReturnsNull()
        {
            _repositoryMock.Setup(r => r.GetTarefaByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Tarefa)null);

            var result = await _service.GetTarefaByIdAsync(999);
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateTarefaAsync_TarefaExists_ReturnsTrue()
        {
            var existingTarefa = new Tarefa
            {
                Id = 1,
                Titulo = "Teste",
                Descricao = "Teste Descrição",
                DataDeVencimento = DateTime.Parse("2025-08-15"),
                Status = TarefaStatus.Pendente
            };

            var tarefaUpdateDto = new TarefaUpdateDto
            {
                Titulo = "Atualizado",
                Descricao = "Atualizado Descrição",
                DataDeVencimento = DateTime.Parse("2025-09-01"),
                Status = TarefaStatus.EmProgresso
            };

            _repositoryMock.Setup(r => r.GetTarefaByIdAsync(existingTarefa.Id))
                .ReturnsAsync(existingTarefa);
            _repositoryMock.Setup(r => r.UpdateTarefaAsync(existingTarefa))
                .Returns(Task.CompletedTask);

            var result = await _service.UpdateTarefaAsync(existingTarefa.Id, tarefaUpdateDto);
            Assert.True(result);
            Assert.Equal(tarefaUpdateDto.Titulo, existingTarefa.Titulo);
            Assert.Equal(tarefaUpdateDto.Status, existingTarefa.Status);
        }

        [Fact]
        public async Task UpdateTarefaAsync_TarefaDoesNotExist_ReturnsFalse()
        {
            _repositoryMock.Setup(r => r.GetTarefaByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Tarefa)null);

            var tarefaUpdateDto = new TarefaUpdateDto
            {
                Titulo = "Atualizado",
                Descricao = "Atualizado Descrição",
                DataDeVencimento = DateTime.Parse("2025-09-01"),
                Status = TarefaStatus.EmProgresso
            };

            var result = await _service.UpdateTarefaAsync(999, tarefaUpdateDto);
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteTarefaAsync_TarefaExists_ReturnsTrue()
        {
            var existingTarefa = new Tarefa
            {
                Id = 1,
                Titulo = "Teste",
                Descricao = "Teste Descrição",
                DataDeVencimento = DateTime.Parse("2025-08-15"),
                Status = TarefaStatus.Pendente
            };

            _repositoryMock.Setup(r => r.GetTarefaByIdAsync(existingTarefa.Id))
                .ReturnsAsync(existingTarefa);
            _repositoryMock.Setup(r => r.DeleteTarefaAsync(existingTarefa))
                .Returns(Task.CompletedTask);

            var result = await _service.DeleteTarefaAsync(existingTarefa.Id);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteTarefaAsync_TarefaDoesNotExist_ReturnsFalse()
        {
            _repositoryMock.Setup(r => r.GetTarefaByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Tarefa)null);

            var result = await _service.DeleteTarefaAsync(999);
            Assert.False(result);
        }

        [Fact]
        public async Task GetTarefasAsync_ReturnsAllTarefas()
        {
            var tarefas = new List<Tarefa>
            {
                new() {
                    Id = 1,
                    Titulo = "Teste 1",
                    Descricao = "Descrição 1",
                    DataDeVencimento = DateTime.Parse("2025-08-15"),
                    Status = TarefaStatus.Pendente
                },
                new() {
                    Id = 2,
                    Titulo = "Teste 2",
                    Descricao = "Descrição 2",
                    DataDeVencimento = DateTime.Parse("2025-08-16"),
                    Status = TarefaStatus.Concluida
                }
            };

            _repositoryMock.Setup(r => r.GetTarefasAsync()).ReturnsAsync(tarefas);

            var result = await _service.GetTarefasAsync();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetTarefasByFilterAsync_WithFilters_ReturnsFilteredTarefas()
        {
            var tarefaList = new List<Tarefa>
            {
                new() {
                    Id = 1,
                    Titulo = "Tarefa 1",
                    Descricao = "Desc 1",
                    DataDeVencimento = DateTime.Parse("2025-08-15"),
                    Status = TarefaStatus.Pendente
                },
                new() {
                    Id = 2,
                    Titulo = "Tarefa 2",
                    Descricao = "Desc 2",
                    DataDeVencimento = DateTime.Parse("2025-08-15"),
                    Status = TarefaStatus.Concluida
                },
                new() {
                    Id = 3,
                    Titulo = "Tarefa 3",
                    Descricao = "Desc 3",
                    DataDeVencimento = DateTime.Parse("2025-08-16"),
                    Status = TarefaStatus.Pendente
                }
            };

            _repositoryMock.Setup(r => r.GetTarefasByFilterAsync(TarefaStatus.Pendente, DateTime.Parse("2025-08-15")))
                           .ReturnsAsync(tarefaList.Where(t => t.Status == TarefaStatus.Pendente &&
                                                                t.DataDeVencimento.HasValue &&
                                                                t.DataDeVencimento.Value.Date == DateTime.Parse("2025-08-15").Date));

            var result = await _service.GetTarefasByFilterAsync(TarefaStatus.Pendente, DateTime.Parse("2025-08-15"));

            Assert.NotNull(result);
            var resultList = result.ToList();
            Assert.Single(resultList);
            Assert.Equal(1, resultList[0].Id);
        }

    }
}
