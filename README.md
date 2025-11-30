# FIAP Games Catalog API

Microsserviço responsável por gerenciar o catálogo de jogos da plataforma FIAP Cloud Games. Oferece endpoints REST para CRUD completo, busca por filtros e integração com autenticação JWT emitida pelo serviço de usuários.

## Tecnologias principais

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core + SQL Server
- Swagger/OpenAPI (Swashbuckle)
- Autenticação JWT (Microsoft.AspNetCore.Authentication.JwtBearer)
- Docker e Docker Compose

## Pré-requisitos

- .NET SDK 8.0
- Docker Desktop (para rodar em containers)
- SQL Server local ou via compose

## Execução local

1. Atualize `FIAP.Games.CatalogAPI/appsettings.Development.json` com:
	- `ConnectionStrings:Catalog` apontando para seu SQL Server.
	- Parâmetros de autenticação (`Authentication.*`).
2. Opcional: aplicar migrations manualmente:
	```bash
	dotnet ef database update --project FIAP.Games.CatalogAPI/FIAP.Games.CatalogAPI.csproj
	```
	(Na execução normal, o `DbSeeder` já chama `Database.Migrate()` automaticamente.)
3. Rodar a API:
	```bash
	dotnet run --project FIAP.Games.CatalogAPI/FIAP.Games.CatalogAPI.csproj
	```
4. Swagger disponível em `https://localhost:<porta>/swagger`.

## Execução com Docker Compose

1. Crie `.env` na raiz com:
	```env
	DB_PASSWORD=YourStrongPassword123
	AUTH_AUTHORITY=https://auth.fiapcloudgames.com
	AUTH_AUDIENCE=catalog-api
	AUTH_REQUIRE_HTTPS=true
	```
2. Suba os serviços (SQL + API):
	```bash
	docker compose up -d
	```
3. Logs:
	```bash
	docker compose logs -f catalogapi
	```
4. API acessível em `http://localhost:5180`.

## Endpoints principais

- `GET /api/games` – Lista jogos com filtros opcionais (`search`, `genre`).
- `GET /api/games/search?title=` – Busca por título (sem necessidade de token).
- `GET /api/games/{id}` – Detalhe de jogo.
- `POST /api/games` – Criação (requer JWT válido).
- `PUT /api/games/{id}` – Atualização (requer JWT).
- `DELETE /api/games/{id}` – Remoção (requer JWT).

## Estrutura relevante

- `Controllers/GamesController.cs` – Endpoints e regras de autorização.
- `Services/` – Camada de negócios e DTOs.
- `Data/CatalogDbContext.cs` & `Data/DbSeeder.cs` – EF Core + migrations automáticas.
- `docker-compose.yml` – Orquestração do SQL Server + API.

## Próximos passos sugeridos

1. Adicionar testes automatizados (unitários e de integração).
2. Configurar observabilidade (logs estruturados, métricas, tracing).
3. Versionar imagens Docker em registry e criar pipeline CI/CD.