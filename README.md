![Servy CI](https://github.com/adrianowsh/curiosity/actions/workflows/pipeline.yml/badge.svg)

# 🚀 Curiosity

> **Curiosity** é uma aplicação backend desenvolvida em **.NET 9**, baseada nos princípios da **Clean Architecture**, com suporte a **CQRS usando MediatR** e autenticação via **Keycloak** como Identity Provider. O projeto está pronto para execução com **Docker Compose**, incluindo o ambiente de desenvolvimento com banco de dados Postgres e Keycloak integrados.

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
  curiosity.api:
    container_name: Curiosity.Api
    image: ${DOCKER_REGISTRY-}curiosityapi
    build:
      context: .
      dockerfile: src/Curiosity.Api/Dockerfile
  curiosity-db:
    image: postgres:latest
    container_name: Curiosity.Db
    environment:
      - POSTGRES_DB=curiosity
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=postgres
    volumes:
      - ./.containers/database:/var/lib/postgresql/data
    ports:
      - 5432:5432

  curiosity-idp:
     image: quay.io/keycloak/keycloak:latest
     container_name: Curiosity.Identity
     command: start-dev --import-realm
     environment:
       - KEYCLOAK_ADMIN=admin
       - KEYCLOAK_ADMIN_PASSWORD=admin
     volumes:
       - ./.containers/identity:/opt/keycloak/data
       - ./.files:/opt/keycloak/data/import/realm.json
     ports:
       - 18080:8080

  curiosity-seq:
    image: datalust/seq:latest
    container_name: Curiosity.Seq
    environment:
      - ACCEPT_EULA=Y
      - SEQ_FIRSTRUN_ADMINPASSWORD=your_secure_password
    ports:
      - 5341:5341
      - 8081:80
  
  curiosity-redis:
    image: redis:latest
    container_name: Curiosity.Redis
    restart: always
    ports:
      - '6379:6379'
```

🔍 Visualização de Logs com Seq

Acesse a interface do Seq em: http://localhost:8081

Admin: admin

Senha: admin

Os logs da aplicação Curiosity serão exibidos automaticamente

✅ Testes

```bash
dotnet test
```

Os testes estão localizados na pasta tests/ e cobrem as camadas de domínio, aplicação e infraestrutura.


✨ Melhorias Futuras

Implentação de Redis para caching

Testes de integração usando Test containers

Testes funcionais

Testes da camada de aplicação com Mocking

Suporte a filas (RabbitMQ, Kafka)

Monitoramento com Prometheus e Grafana

CI/CD com GitHub Actions

Deploy com Docker Swarm ou Kubernetes

📝 Licença

Este projeto está licenciado sob os termos da licença MIT


📌 Decisões Técnicas

Este projeto foi desenvolvido com foco em escalabilidade, organização, segurança e performance. Abaixo estão algumas decisões arquiteturais e técnicas adotadas:

🔄 Uso de CQRS com MediatR

A separação entre comandos (Command) e consultas (Query) promove uma divisão clara entre escrita e leitura, facilitando a escalabilidade e a manutenção.

O MediatR fornece um ponto único de orquestração para requisições, removendo acoplamento direto entre controladores e regras de negócio.

🔁 Pipeline Behaviors com MediatR

Foram implementados Pipeline Behaviors para lidar com preocupações transversais de forma centralizada:

ValidationBehavior: Validação de entrada com FluentValidation

LoggingBehavior: Logging de execução de handlers

Esses comportamentos permitem aplicar boas práticas de cross-cutting sem poluir os handlers.

services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

🔐 Uso de internal para restrição de acesso

Classes que não precisam ser expostas publicamente (ex: repositórios, implementações internas) são marcadas como internal para:

Reduzir acoplamento entre camadas

Melhorar a segurança e encapsulamento

Aumentar a performance da compilação

Onde necessário, InternalsVisibleTo é utilizado para permitir o acesso via projetos de teste.

🧪 Centralização da Validação com FluentValidation

Toda validação de entrada é feita via FluentValidation, evitando validações manuais em controladores ou handlers.

Os validators são registrados automaticamente e executados via ValidationBehavior.

🧱 Separação de Responsabilidades com Clean Architecture

A arquitetura do projeto está organizada em quatro camadas principais:

Domain: Regras e modelos de negócio puros

Application: Casos de uso, lógica de aplicação, CQRS

Infrastructure: Implementações de acesso a dados e serviços externos

WebAPI: Entrada da aplicação, configurações e autenticação

A injeção de dependência respeita os limites de cada camada.

❌ Não uso de AutoMapper

Por questões de clareza, performance e previsibilidade, optou-se por não utilizar AutoMapper.

As conversões entre entidades e DTOs são feitas manualmente, com lógica explícita.

```csharp
return new UserDto
{
    Id = user.Id,
    Name = user.Name,
    Email = user.Email
};
```

📈 Logging estruturado com Serilog + Seq

Toda a aplicação utiliza Serilog para logging estruturado.

Os logs são enviados para:

Console (útil para desenvolvimento local)

Seq (útil para observabilidade e análise em tempo real)

A visualização é feita acessando: http://localhost:8081

Essas decisões foram tomadas com base em boas práticas de arquitetura de software e nas necessidades reais do projeto. Novas decisões poderão ser documentadas conforme o projeto evolui.




🔐 Como registrar um usuário para autenticação

Antes de utilizar a aplicação, é necessário registrar um usuário válido para autenticação. Para isso, siga os passos abaixo:

1. Importe a coleção Curiosity.postman_collection no Postman

Abra o Postman

Clique em Importar

Selecione o arquivo Curiosity.postman_collection fornecido com o projeto

2. Chame a rota de registro

Após importar a coleção, localize a requisição:

POST Register


Com o endpoint:

https://localhost:5001/api/v1/user/register


Preencha o corpo da requisição com os dados do novo usuário, por exemplo:

```json
{
    "email": "user1@curioisity.com",
    "name": "User1",
    "password": "asdf"
}
```

Execute a requisição para criar o usuário.

No front end, utilize as credenciais do usuário criado para fazer login e obter o token JWT.

```
user:  "user1@curioisity.com"
password: "asdf"
```

Esse usuário será necessário para se autenticar na aplicação futuramente.
.