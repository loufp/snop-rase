# Snop-Rase

> **Snop-Rase** — backend-проект prediction market платформы с фокусом на private rooms, торговые сценарии и инженерные практики для построения распределённых систем.

Snop-Rase вдохновлён идеей платформ вроде Polymarket, но в первую очередь ориентирован на **закрытые комнаты**: небольшие сообщества, друзей и команды, которые хотят создавать свои рынки и спорить на исходы событий.

---

## Что уже есть в проекте

Текущий этап — фундамент backend:

- регистрация и логин пользователей;
- JWT-аутентификация;
- хэширование паролей;
- PostgreSQL + Entity Framework Core;
- базовая архитектура по слоям (Api / Application / Domain / Infrastructure);
- unit-тесты для доменной и application-логики.

Основные API-эндпоинты:

- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/auth/logout` (требует JWT)

---

## Цель проекта

Главная цель Snop-Rase — не просто CRUD, а постепенная разработка сложной prediction market системы с упором на:

- concurrent operations и консистентность балансов;
- event-driven подход;
- CQRS и доменную модель;
- масштабируемую микросервисную архитектуру;
- наблюдаемость и операционную прозрачность.

---

## Архитектурный вектор

План развития:

```text
Client
  ↓
API Layer
  ↓
Application (Commands/Queries)
  ↓
Domain
  ↓
Infrastructure (DB, Security, Messaging)
```

В дальнейшем проект расширяется до сервисной модели: Auth, Rooms, Markets, Trading, Wallet, Settlement, Notifications.

---

## Технологический стек

### Backend
- C#
- .NET / ASP.NET Core
- MediatR
- FluentValidation
- Serilog

### Data
- PostgreSQL
- Entity Framework Core

### Security
- JWT
- BCrypt

### Testing
- xUnit

---

## Структура репозитория

```text
snop-rase/
├── snope/
│   ├── snope-rase/                # host (Program, appsettings)
│   ├── snoperase.Api/             # controllers
│   ├── snoperase.Application/     # use-cases, commands, handlers
│   ├── snoperase.Domain/          # сущности и правила домена
│   └── snoperase.Infastrucure/    # EF Core, репозитории, security
├── TestSnope/                     # тесты
├── TestSnopeX/                    # тесты
└── docker-compose.yml             # локальный PostgreSQL
```

---

## Roadmap

### Phase 1 — Foundation (в процессе)
- [x] Auth (register/login/JWT)
- [x] User entity + persistence
- [x] PostgreSQL local setup
- [ ] Refresh tokens
- [ ] User profile/statistics

### Phase 2 — Rooms & Invitations
- [ ] Public/private rooms
- [ ] Room members и роли
- [ ] Invite links / invitations

### Phase 3 — Markets
- [ ] Создание и управление рынками
- [ ] Market lifecycle (Draft/Open/Closed/Resolved/Cancelled)
- [ ] История рынков

### Phase 4 — Trading Core
- [ ] Orders (Market/Limit)
- [ ] Order book
- [ ] Matching engine
- [ ] Trades / Positions / PnL

### Phase 5 — Wallet & Settlement
- [ ] Ledger entries
- [ ] Settlement pipeline
- [ ] Balance consistency guarantees

### Phase 6 — Disputes, Realtime, Infra
- [ ] Dispute workflow
- [ ] Realtime updates (SignalR/WebSocket)
- [ ] Messaging (RabbitMQ/Kafka)
- [ ] Observability (OpenTelemetry/Prometheus/Grafana)
- [ ] Docker/Kubernetes deployment

---

## Локальный запуск

1. Поднять PostgreSQL:

```bash
docker compose up -d
```

2. Запустить API:

```bash
dotnet run --project /home/runner/work/snop-rase/snop-rase/snope/snope-rase/snope-rase.csproj
```

3. Swagger доступен в Development-режиме.

---

## Статус

🚧 **Work in Progress**

Проект находится в активной разработке и используется как инженерная площадка для проработки архитектуры и торговой логики prediction market системы.
