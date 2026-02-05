# API de Gerenciamento de Clientes

Uma API RESTful desenvolvida em C# com .NET 8 para gerenciar informações de clientes com persistência em banco de dados.

## 📋 Como Rodar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download) ou superior
- Visual Studio, Visual Studio Code ou editor de sua preferência

### Passos de Execução

1. **Clone ou navegue até o diretório do projeto**

   ```bash
   cd src
   ```

2. **Restaure as dependências do projeto**

   ```bash
   dotnet restore
   ```

3. **Aplique as migrations do banco de dados**

   ```bash
   dotnet ef database update
   ```

4. **Execute a aplicação**

   ```bash
   dotnet run --project Clientes.Api
   ```

5. **Acesse a API**
   - A API estará disponível em: `http://localhost:5000` ou `https://localhost:5001`
   - Swagger UI: `http://localhost:5000/` (em desenvolvimento)

## 🛠️ Tecnologias Utilizadas

| Tecnologia                | Versão | Descrição                                 |
| ------------------------- | ------ | ----------------------------------------- |
| **.NET**                  | 8.0    | Framework web moderno da Microsoft        |
| **ASP.NET Core**          | 8.0.23 | Framework para construção de APIs RESTful |
| **Entity Framework Core** | 8.0.23 | ORM para acesso ao banco de dados         |
| **SQLite**                | -      | Banco de dados relacional leve            |
| **Swagger/Swashbuckle**   | 6.6.2  | Documentação automática da API            |

## 🎯 Decisões Técnicas

### 1. **Arquitetura em Camadas**

- **Controllers**: Responsáveis pelos endpoints HTTP
- **Models**: Entidades de domínio com validações
- **Data (DbContext)**: Acesso e persistência de dados
- **Migrations**: Controle de versão do schema do banco

Essa separação promove manutenibilidade, testabilidade e separação de responsabilidades.

### 2. **Entity Framework Core + SQLite**

- **Escolha**: ORM moderno com migrations automáticas
- **Benefício**: Abstração do banco de dados, fácil migração entre SGBDs no futuro
- **SQLite**: Ideal para prototipagem e ambientes de desenvolvimento, arquivo único (`database.db`)

### 3. **Validação de Dados**

- Validações implementadas direto no modelo usando `[Required]` e `[EmailAddress]`
- Validações lógicas no controller (duplicidade de email e ID)
- Retorna mensagens de erro estruturadas (HTTP 409 Conflict)

**Benefício**: Garante integridade dos dados antes de persistir no banco.

### 4. **Swagger/OpenAPI**

- Documentação automática dos endpoints
- Interface interativa para testar a API
- Reduz necessidade de documentação manual

### 5. **Null Safety (Nullable Context)**

- Habilitado no projeto (`<Nullable>enable</Nullable>`)
- Previne `NullReferenceException` em tempo de compilação
- Código mais robusto e seguro

### 6. **GUID para IDs**

- IDs primários gerados como `Guid` (UUID)
- Distribuído entre cliente e servidor sem risco de colisão
- Melhor segurança que IDs sequenciais

## 📡 Endpoints Disponíveis

### GET `/api/clientes`

Retorna a lista de todos os clientes cadastrados.

### POST `/api/clientes`

Cria um novo cliente.

**Body (JSON):**

```json
{
  "nome": "João Silva",
  "email": "joao@example.com"
}
```

**Respostas:**

- `201 Created`: Cliente criado com sucesso
- `409 Conflict`: Cliente ou email já existente
