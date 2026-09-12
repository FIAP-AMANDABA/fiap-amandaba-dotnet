# Amandaba API

API REST desenvolvida em ASP.NET Core para o projeto Amandaba, uma aplicação voltada ao gerenciamento e acompanhamento da saúde de pets.

A API permite centralizar informações relacionadas aos animais de um tutor, incluindo dados cadastrais, histórico de peso, vacinas, doenças, alergias, medicamentos, consultas, exames e, agora, um plano de cuidados personalizado gerado por Inteligência Artificial.

O projeto utiliza Oracle como banco de dados e Entity Framework Core para acesso aos dados, seguindo uma arquitetura organizada em camadas. Também possui documentação interativa através do Swagger, monitoramento da aplicação, logging estruturado, tracing, métricas e testes automatizados.

---

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- Oracle Database
- Oracle.EntityFrameworkCore
- Swagger / OpenAPI
- Serilog
- Health Checks
- OpenTelemetry
- Azure Monitor
- Application Insights
- xUnit
- Moq
- Entity Framework Core InMemory
- WebApplicationFactory
- Google Gemini API (IA Generativa / LLM)
- HttpClient (integração com serviços externos)

---

## Estrutura do Projeto

A aplicação está organizada em camadas para separar as responsabilidades:

```text
Amandaba.API
├── Application
│   ├── Dtos
│   ├── Interfaces
│   ├── Mappers
│   └── UseCases
│       └── GeminiUseCase.cs
├── Domain
│   ├── Entities
│   └── Interfaces
├── Infrastructure
│   └── Data
│       ├── Migrations
│       └── Repositories
├── Presentation
│   └── Controllers
├── Properties
├── appsettings.json
└── Program.cs

Amandaba.Tests
├── App
└── Integration
```

O fluxo principal da aplicação segue:

```text
Controller → UseCase → Repository → Entity Framework Core → Oracle
```

Para a funcionalidade de IA, o fluxo é:

```text
Controller → UseCase (dados do pet) → GeminiUseCase → API Google Gemini → Controller
```

---

# Funcionalidades

A API disponibiliza funcionalidades para gerenciamento das principais informações relacionadas à saúde do pet.

### Pets

Permite cadastrar pets, consultar seus dados, listar os pets associados a um tutor, editar informações cadastrais e ativar ou inativar um pet.

### Espécies

Disponibiliza o catálogo de espécies utilizadas no cadastro dos pets.

### Histórico de Peso

Permite registrar e acompanhar as medições de peso do pet, incluindo a consulta da medição mais recente.

### Vacinas

Disponibiliza o catálogo de vacinas e permite registrar e acompanhar as aplicações realizadas em cada pet.

### Doenças

Permite registrar e manter o histórico de doenças e condições de saúde do pet.

### Alergias

Permite registrar e manter as alergias identificadas no pet.

### Medicamentos

Permite controlar medicamentos utilizados pelo pet e acompanhar seu status.

### Consultas

Permite registrar consultas veterinárias e acompanhar seu status.

### Exames

Permite registrar exames do pet e acompanhar seu status.

### Plano de Cuidados com IA Generativa (novo)

Gera, sob demanda, um plano de cuidados personalizado para o pet, cruzando dados clínicos reais (medicamentos em uso, alergias e doenças) através de um modelo de linguagem (LLM), auxiliando o tutor a compreender melhor as orientações e reduzindo dúvidas recorrentes na clínica.

---

# Endpoints

## Pets

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/tutores/{idTutor}/pets` | Lista os pets associados a um tutor |
| POST | `/api/tutores/{idTutor}/pets` | Cadastra um novo pet |
| GET | `/api/pets/{petId}` | Consulta os dados de um pet |
| PUT | `/api/pets/{petId}` | Atualiza os dados cadastrais do pet |
| PATCH | `/api/pets/{petId}/status` | Ativa ou inativa um pet |
| GET | `/api/pets/{petId}/plano-cuidados` | Gera um plano de cuidados personalizado via IA Generativa |

## Espécies

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/especies` | Lista as espécies disponíveis |

## Histórico de Peso

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/pets/{petId}/pesos` | Lista o histórico de peso do pet |
| GET | `/api/pets/{petId}/pesos/atual` | Retorna a medição de peso mais recente |
| POST | `/api/pets/{petId}/pesos` | Registra uma nova medição |
| PUT | `/api/pets/{petId}/pesos/{pesoId}` | Atualiza uma medição |
| DELETE | `/api/pets/{petId}/pesos/{pesoId}` | Exclui uma medição |

## Catálogo de Vacinas

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/vacinas` | Lista o catálogo de vacinas |
| GET | `/api/vacinas/{vacinaId}` | Consulta uma vacina do catálogo |

## Vacinas do Pet

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/pets/{petId}/vacinas` | Lista as vacinas aplicadas no pet |
| GET | `/api/pets/{petId}/vacinas/{aplicacaoId}` | Consulta uma aplicação de vacina |
| POST | `/api/pets/{petId}/vacinas` | Registra uma aplicação de vacina |
| PUT | `/api/pets/{petId}/vacinas/{aplicacaoId}` | Atualiza uma aplicação |
| DELETE | `/api/pets/{petId}/vacinas/{aplicacaoId}` | Exclui uma aplicação |

## Doenças

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/pets/{petId}/doencas` | Lista as doenças do pet |
| GET | `/api/pets/{petId}/doencas/{doencaId}` | Consulta uma doença |
| POST | `/api/pets/{petId}/doencas` | Cadastra uma doença |
| PUT | `/api/pets/{petId}/doencas/{doencaId}` | Atualiza uma doença |
| DELETE | `/api/pets/{petId}/doencas/{doencaId}` | Exclui uma doença |

## Alergias

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/pets/{petId}/alergias` | Lista as alergias do pet |
| GET | `/api/pets/{petId}/alergias/{alergiaId}` | Consulta uma alergia |
| POST | `/api/pets/{petId}/alergias` | Cadastra uma alergia |
| PUT | `/api/pets/{petId}/alergias/{alergiaId}` | Atualiza uma alergia |
| DELETE | `/api/pets/{petId}/alergias/{alergiaId}` | Exclui uma alergia |

## Medicamentos

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/pets/{petId}/medicamentos` | Lista os medicamentos do pet |
| GET | `/api/pets/{petId}/medicamentos?status={status}` | Filtra medicamentos por status |
| GET | `/api/pets/{petId}/medicamentos/{medicamentoId}` | Consulta um medicamento |
| POST | `/api/pets/{petId}/medicamentos` | Cadastra um medicamento |
| PUT | `/api/pets/{petId}/medicamentos/{medicamentoId}` | Atualiza um medicamento |
| PATCH | `/api/pets/{petId}/medicamentos/{medicamentoId}/status` | Altera o status do medicamento |
| DELETE | `/api/pets/{petId}/medicamentos/{medicamentoId}` | Exclui um medicamento |

Status disponíveis:

```text
EM_USO
CONCLUIDO
SUSPENSO
```

## Consultas

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/pets/{petId}/consultas` | Lista as consultas do pet |
| GET | `/api/pets/{petId}/consultas?status={status}` | Filtra consultas por status |
| GET | `/api/pets/{petId}/consultas/{consultaId}` | Consulta um registro |
| POST | `/api/pets/{petId}/consultas` | Cadastra uma consulta |
| PUT | `/api/pets/{petId}/consultas/{consultaId}` | Atualiza uma consulta |
| PATCH | `/api/pets/{petId}/consultas/{consultaId}/status` | Altera o status da consulta |
| DELETE | `/api/pets/{petId}/consultas/{consultaId}` | Exclui uma consulta |

Status disponíveis:

```text
AGENDADA
REALIZADA
CANCELADA
```

## Exames

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/pets/{petId}/exames` | Lista os exames do pet |
| GET | `/api/pets/{petId}/exames?status={status}` | Filtra exames por status |
| GET | `/api/pets/{petId}/exames/{exameId}` | Consulta um exame |
| POST | `/api/pets/{petId}/exames` | Cadastra um exame |
| PUT | `/api/pets/{petId}/exames/{exameId}` | Atualiza um exame |
| PATCH | `/api/pets/{petId}/exames/{exameId}/status` | Altera o status do exame |
| DELETE | `/api/pets/{petId}/exames/{exameId}` | Exclui um exame |

Status disponíveis:

```text
SOLICITADO
REALIZADO
RESULTADO_DISPONIVEL
```

---

# Integração com IA Generativa (Plano de Cuidados)

## Problema

Tutores frequentemente esquecem as orientações passadas após a consulta ou têm dúvidas sobre como administrar medicamentos e lidar com o quadro clínico do pet (doenças e alergias associadas), o que gera dúvidas recorrentes e sobrecarrega a equipe da clínica.

## Solução

Foi implementado um **Gerador de Plano de Cuidados Personalizado**, que utiliza IA Generativa (LLM) para produzir, sob demanda, um plano de cuidados coerente com o histórico clínico real do pet.

## Abordagem de IA

- **Tipo:** IA Generativa (LLM).
- **Modelo:** Google Gemini (`gemini-pro`), via API gratuita do Google AI Studio.
- **Justificativa da escolha do modelo:** o `gemini-pro` apresentou maior estabilidade de fila na camada gratuita em comparação ao `gemini-2.5-flash` durante os testes realizados.

## Configuração

No `appsettings.json` foi adicionada a seção `"Gemini"`, responsável por armazenar a `ApiKey` e a `Url` do endpoint utilizado:

```json
{
  "Gemini": {
    "ApiKey": "SUA_API_KEY",
    "Url": "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent"
  }
}
```

> A chave de API não é versionada no repositório e deve ser configurada localmente, seguindo o mesmo princípio adotado para a connection string do Oracle.

No `Program.cs`, foi configurado o `HttpClient` e registrado o serviço responsável pela integração com a IA (`GeminiUseCase` / `GeminiService`).

## Camada de Aplicação — `GeminiUseCase`

Serviço responsável exclusivamente por encapsular a comunicação HTTP com o Google AI Studio, incluindo tratamento de erros que lê e expõe o corpo da resposta (JSON) retornada pelo Google quando a requisição falha (por exemplo, `404 Not Found` ou `503 Service Unavailable`), facilitando o diagnóstico de problemas.

## Camada de Apresentação — `PetsController`

Novo endpoint:

```http
GET /api/pets/{petId}/plano-cuidados
```

Documentado no Swagger com `Summary`, `Description` e os `StatusCodes` de resposta.

O endpoint utiliza `_petUseCase.ObterPorId(petId)` para obter os dados reais do pet (nome e espécie), eliminando o uso de dados fixos ("chumbados") no código.

### Cruzamento de dados clínicos

Para montar um prompt mais completo, foi criado um método auxiliar `ObterDadosDinamicos`, que utiliza *Reflection* para buscar, nos respectivos UseCases, o histórico de:

- Medicamentos em uso;
- Alergias registradas;
- Doenças e condições de saúde.

Essa abordagem evita acoplamento rígido no construtor do `PetsController`. Os dados relacionais vindos do Oracle (via Entity Framework) são serializados em JSON e injetados dinamicamente no prompt enviado à IA.

O *system prompt* instrui o modelo a atuar como um sistema de apoio veterinário, cruzando ativamente as informações — por exemplo, analisando os medicamentos em uso em conjunto com as alergias ativas — para gerar um plano de cuidados seguro e coerente com o quadro clínico do pet.

## Fluxo de execução

```text
Requisição HTTP (GET /api/pets/{petId}/plano-cuidados)
        ↓
PetsController
        ↓
PetUseCase (dados cadastrais do pet)
        ↓
ObterDadosDinamicos (Reflection → Medicamentos, Alergias, Doenças)
        ↓
GeminiUseCase (monta prompt e chama a API do Google)
        ↓
API Google Gemini
        ↓
Resposta processada e retornada ao tutor
```

## Validação

- O fluxo completo (Controller → UseCase → API externa → Controller) foi validado via Swagger em ambiente local.
- Os logs estruturados com Serilog incluem `TraceId` e `SpanId`, permitindo correlacionar as requisições da aplicação.
- As requisições e chamadas HTTP instrumentadas podem ser acompanhadas através do Azure Monitor/Application Insights.

---

# Monitoramento e Observabilidade

A aplicação possui recursos de monitoramento e observabilidade implementados através de Health Checks, Serilog, OpenTelemetry, Azure Monitor e Application Insights.

## Health Checks

Os Health Checks permitem verificar separadamente a disponibilidade da API e a conexão com o banco Oracle.

### Verificar a API

```http
GET /health/live
```

Verifica se a aplicação está em execução.

Também está disponível através do controller:

```http
GET /api/health/live
```

### Verificar o banco de dados

```http
GET /health/db
```

Verifica a conectividade da aplicação com o Oracle.

Também está disponível através do controller:

```http
GET /api/health/db
```

Quando o Health Check executado pelo controller está saudável, a API retorna `200 OK`. Caso o recurso verificado esteja indisponível, retorna `503 Service Unavailable`.

## Logging

O logging estruturado da aplicação utiliza Serilog.

Os logs são enviados para:

- console da aplicação;
- arquivos de log com rotação diária.

São utilizados os níveis:

- `Information`
- `Warning`
- `Error`

Os logs também incluem `TraceId` e `SpanId`, permitindo correlacionar as informações registradas com o tracing das requisições.

## Tracing e Métricas

A aplicação utiliza **OpenTelemetry integrado ao Azure Monitor/Application Insights** para tracing e coleta de métricas.

A configuração é realizada no `Program.cs` através do Azure Monitor:

```csharp
builder.Services.AddOpenTelemetry()
    .UseAzureMonitor(options =>
    {
        options.ConnectionString =
            builder.Configuration["ApplicationInsights:ConnectionString"];
    });
```

A Connection String do Application Insights é configurada no ambiente local através do `appsettings.json`:

```json
{
  "ApplicationInsights": {
    "ConnectionString": "SUA_CONNECTION_STRING"
  }
}
```

> A Connection String do Application Insights deve ser configurada de acordo com o recurso criado no Azure.

A telemetria coletada é enviada ao **Application Insights**, permitindo acompanhar as requisições da API e informações de desempenho.

Os traces permitem acompanhar informações como:

- `TraceId`;
- `SpanId`;
- endpoint acessado;
- método HTTP;
- status da resposta;
- duração da requisição.

No Application Insights é possível acompanhar, entre outras informações:

- quantidade de requisições recebidas;
- requisições com falha;
- tempo de resposta do servidor;
- traces das requisições;
- duração das operações;
- status das requisições.

A correlação entre os logs locais e a telemetria do Azure pode ser realizada através do `TraceId`, permitindo acompanhar uma mesma requisição entre os registros da aplicação e o Application Insights.

O monitoramento pode ser realizado no recurso do Application Insights no Portal do Azure, através das áreas de **Search**, **Performance**, **Failures**, **Live Metrics** e **Application Map**.

---

# Testes Automatizados

O projeto possui testes automatizados utilizando xUnit.

Os testes estão organizados em:

```text
Amandaba.Tests
├── App
│   ├── PetUseCaseTest.cs
│   └── PetRepositoryTest.cs
│
└── Integration
    ├── AmandabaApiFactory.cs
    ├── IntegrationTestCollection.cs
    └── PetsControllerIntegrationTest.cs
```

## Testes de Aplicação

Os testes do `PetUseCase` utilizam:

- xUnit;
- padrão AAA (Arrange, Act, Assert);
- Moq para simulação das dependências.

## Testes de Repository

Os testes do `PetRepository` utilizam Entity Framework Core InMemory para testar as operações do repositório em um banco isolado.

## Testes de Integração

Os testes de integração utilizam `WebApplicationFactory`.

Durante os testes de integração, o Oracle é substituído pelo Entity Framework Core InMemory, permitindo executar os testes de forma isolada.

O restante do fluxo utiliza os componentes reais da aplicação:

```text
Requisição HTTP
        ↓
Controller
        ↓
UseCase
        ↓
Repository
        ↓
Entity Framework Core InMemory
        ↓
Resposta HTTP
```

São validados cenários de:

- resposta `200 OK`;
- resposta `404 Not Found`;
- atualização através de `PATCH`;
- resposta `204 No Content`;
- persistência da alteração realizada.

A suíte atual possui 22 testes automatizados.

---

# Executando os Testes

A partir da pasta raiz da solução, execute:

```bash
dotnet test
```

O comando restaura e compila os projetos necessários e executa a suíte de testes.

Também é possível executar os testes através do Gerenciador de Testes do Visual Studio.

---

# Instalação e Execução

## Pré-requisitos

Para executar o projeto é necessário possuir:

- .NET 8 SDK;
- acesso a um banco Oracle compatível com a estrutura utilizada pelo projeto;
- uma chave de API do Google Gemini (Google AI Studio), para a funcionalidade de plano de cuidados;
- uma Connection String válida de um recurso Application Insights no Azure, para envio da telemetria;
- Git;
- Visual Studio ou outra IDE compatível com projetos ASP.NET Core.

## 1. Clonar o repositório

```bash
git clone https://github.com/FIAP-AMANDABA/fiap-amandaba-dotnet.git
```

Entre na pasta do projeto:

```bash
cd fiap-amandaba-dotnet
```

## 2. Restaurar as dependências

```bash
dotnet restore
```

## 3. Configurar a conexão com o Oracle

Por segurança, as credenciais do banco de dados não são armazenadas no repositório.

Configure a connection string `Oracle` no ambiente local antes de executar a aplicação.

Exemplo de estrutura:

```json
{
  "ConnectionStrings": {
    "Oracle": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=SEU_DATA_SOURCE"
  }
}
```

Substitua os valores de exemplo pelas credenciais e pelo Data Source correspondentes ao ambiente Oracle utilizado.

## 4. Configurar a integração com o Google Gemini

Da mesma forma, configure a seção `Gemini` no ambiente local:

```json
{
  "Gemini": {
    "ApiKey": "SUA_API_KEY",
    "Url": "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent"
  }
}
```

## 5. Configurar o Application Insights

Para habilitar o envio de tracing e métricas ao Azure, configure a Connection String do recurso Application Insights:

```json
{
  "ApplicationInsights": {
    "ConnectionString": "SUA_CONNECTION_STRING"
  }
}
```

A Connection String pode ser obtida na página **Overview** do recurso Application Insights no Portal do Azure.

## 6. Executar a API

Na pasta do projeto da API:

```bash
cd Amandaba.API
```

Execute:

```bash
dotnet run
```

A aplicação exibirá no console os endereços HTTP/HTTPS utilizados durante a execução.

---

# Swagger

Em ambiente de desenvolvimento, a API disponibiliza documentação interativa através do Swagger.

Com a aplicação em execução, acesse:

```text
https://localhost:{porta}/swagger
```

A porta utilizada deve ser consultada no console durante a inicialização da aplicação.

Através do Swagger é possível visualizar e testar os endpoints disponibilizados pela API, incluindo o endpoint de geração do plano de cuidados com IA.

---

# Respostas HTTP

A API utiliza os principais códigos HTTP de acordo com o resultado das operações:

| Código | Significado |
|---|---|
| `200 OK` | Operação de consulta realizada com sucesso |
| `201 Created` | Recurso cadastrado com sucesso |
| `204 No Content` | Atualização ou exclusão realizada com sucesso |
| `400 Bad Request` | Dados ou parâmetros inválidos |
| `404 Not Found` | Recurso não encontrado |
| `503 Service Unavailable` | Falha detectada pelo Health Check |

---

# Projeto Acadêmico

Projeto desenvolvido como parte do Challenge da FIAP para a disciplina **Advanced Business Development with .NET**, com a funcionalidade de IA Generativa desenvolvida para a disciplina **Disruptive Architectures: IoT, IoB & Generative IA**.