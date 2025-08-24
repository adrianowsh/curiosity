![Servy CI](https://github.com/adrianowsh/curiosity/actions/workflows/pipeline.yml/badge.svg)

# 🚀 Curiosity

> **Curiosity** é uma aplicação backend desenvolvida em **.NET 9**, baseada nos princípios da **Clean Architecture**, com suporte a **CQRS usando MediatR** e autenticação via **Keycloak** como Identity Provider. O projeto está pronto para execução com **Docker Compose**, incluindo o ambiente de desenvolvimento com banco de dados e Keycloak integrados.

---

## 🏗️ Arquitetura

src/
├── Domain/ -> Entidades, enums, interfaces e lógica de negócio pura
├── Application/ -> Casos de uso (Use Cases), comandos, consultas, validações (CQRS + MediatR)
├── Infrastructure/ -> Banco de dados, repositórios e serviços externos
├── WebAPI/ -> Controllers, middlewares, autenticação, configurações da API


---

## 🛠️ Tecnologias e Padrões

- **.NET 9**
- **Clean Architecture**
- **CQRS com MediatR**
- **Entity Framework Core**
- **FluentValidation**
- **Serilog**
- **Swagger (Swashbuckle)**
- **Keycloak** (OIDC)
- **Docker + Docker Compose**
- **xUnit / Moq** (testes)

---

## ✅ Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/)

---

## ⚙️ Configuração com Docker Compose

O projeto inclui um arquivo `docker-compose.yml` com os seguintes serviços:

- **Curiosity WebAPI** (`webapi`)
- **Banco de dados (PostgreSQL)** (`db`)
- **Keycloak** (`keycloak`)

### 🔄 Subindo os serviços

```bash
docker-compose up --build
```

A aplicação estará acessível em:

API: http://localhost:5000

Swagger: http://localhost:5000/swagger

Keycloak: http://localhost:8080

👤 Acesso ao Keycloak

URL: http://localhost:8080

Realm: curiosity

Admin: admin / admin

Client: curiosity-api (tipo: bearer-only, usado pela API)

Usuário de teste: testuser / Test@123

🔐 Configuração de Autenticação
A API utiliza autenticação JWT integrada com o Keycloak.

```JSON
{
  "Authentication": {
    "Authority": "http://keycloak:8080/realms/curiosity",
    "Audience": "curiosity-api"
  }
}
```

Registro no Program.cs:

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Authentication:Authority"];
        options.Audience = builder.Configuration["Authentication:Audience"];
        options.RequireHttpsMetadata = false;
    });

🧪 Testando com Token do Keycloak

1. Faça login via Keycloak ou obtenha um token JWT via API:

```bash
curl -X POST http://localhost:8080/realms/curiosity/protocol/openid-connect/token \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "client_id=curiosity-api" \
  -d "username=testuser" \
  -d "password=Test@123" \
  -d "grant_type=password"
```

2. Use o token para acessar endpoints protegidos da API:

```bash
curl -H "Authorization: Bearer {token}" http://localhost:5000/api/users
```

```csharp
public record CreateUserCommand(string Name, string Email) : IRequest<Guid>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _repository;

    public CreateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name, request.Email);
        await _repository.AddAsync(user);
        return user.Id;
    }
}
```

🐳 docker-compose.yml (exemplo incluído no projeto)

```yaml
version: "3.9"

services:

  db:
    image: postgres:15
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
      POSTGRES_DB: curiositydb
    ports:
      - "5432:5432"
    volumes:
      - pgdata:/var/lib/postgresql/data

  keycloak:
    image: quay.io/keycloak/keycloak:24.0.1
    command: start-dev --import-realm
    environment:
      KEYCLOAK_ADMIN: admin
      KEYCLOAK_ADMIN_PASSWORD: admin
    ports:
      - "8080:8080"
    volumes:
      - ./keycloak/realm-export.json:/opt/keycloak/data/import/realm-export.json

  webapi:
    build:
      context: .
      dockerfile: WebAPI/Dockerfile
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
    ports:
      - "5000:80"
    depends_on:
      - db
      - keycloak

volumes:
  pgdata:
```

✅ Testes

```bash
dotnet test
```

Os testes estão localizados na pasta tests/ e cobrem as camadas de domínio, aplicação e infraestrutura.


✨ Melhorias Futuras

Versionamento da API

Suporte a filas (RabbitMQ, Kafka)

Monitoramento com Prometheus e Grafana

CI/CD com GitHub Actions

Deploy com Docker Swarm ou Kubernetes

📝 Licença

Este projeto está licenciado sob os termos da licença MIT
.