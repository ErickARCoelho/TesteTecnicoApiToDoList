# TesteTecnicoApiToDoList
Avaliação Técnica
Esta é uma API desenvolvida em .NET 8 para gerenciamento de tarefas. A aplicação permite criar, editar, excluir, listar e filtrar tarefas. Foi implementada com foco em boas práticas de desenvolvimento, utilizando uma arquitetura em camadas, tratamento global de erros, logging básico, documentação via Swagger e testes automatizados com xUnit e Moq.

## Arquitetura e Decisão do Modelo

### Estrutura em Camadas

O projeto foi organizado em camadas para promover a separação de responsabilidades e facilitar a manutenção, escalabilidade e testabilidade:

- **Domain:** Contém as entidades e enums (por exemplo, a entidade `Tarefa` e o enum `TarefaStatus`).
- **DTOs:** Define os objetos de transferência de dados (ex.: `TarefaCreateDto`, `TarefaUpdateDto`, `TarefaReadDto` e `TarefaFilterDto`).
- **Repositories:** Responsável pelo acesso a dados utilizando o Entity Framework Core com um banco InMemory.
- **Services:** Contém a lógica de negócio e realiza o mapeamento entre DTOs e entidades, aplicando os princípios SOLID.
- **Controllers:** Exponha os endpoints RESTful, utilizando os serviços e gerenciando as requisições HTTP.
- **Middleware:** Implementa o tratamento global de erros, capturando exceções não tratadas e realizando logging básico.
- **Testes:** A camada de testes utiliza xUnit e Moq para validar a lógica da camada de serviços.

### Por Que Essa Arquitetura?

- **Separação de Responsabilidades:** Cada camada tem uma responsabilidade única, facilitando a manutenção e o desenvolvimento incremental.
- **Testabilidade:** A utilização de injeção de dependência e interfaces permite isolar cada camada, tornando os testes unitários mais simples e eficazes.
- **Escalabilidade:** Caso seja necessário migrar de um banco InMemory para um banco de dados real, a abstração do repositório minimiza alterações na lógica de negócio.
- **Boas Práticas:** O uso de padrões como REST, middleware para tratamento de erros e documentação via Swagger melhora a qualidade do código e a experiência do desenvolvedor.

## Como Rodar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (opcional, mas recomendado)
- Acesso à internet para restaurar os pacotes NuGet.

### Instruções

1. **Clonar o Repositório**

   bash
   git clone https://github.com/ErickARCoelho/TesteTecnicoApiToDoList.git
   cd SEU_REPOSITORIO

2. **Clonar o Repositório** dotnet restore

3. **Compilar o Projeto** dotnet build

### Como Testar as Funcionalidades

1. **Testes Automatizados** O projeto conta com testes unitários que validam a lógica dos serviços. Navegar até o Projeto de Testes abra um terminal e execute cd GestaoTarefas.Tests
2. **Testes Manuais** Você pode testar a API manualmente usando o Swagger ou ferramentas como Postman