# API Gateway с агрегацией данных и кэшированием

## Запуск

docker-compose up --build -d

## Проверка

API Gateway: http://localhost:8080/api/profile/123

Seq logs: http://localhost:5341 (логин/пароль для первого входа: admin/Admin123!)

Redis: работает на порту 6379

## Для полного сброса докер томов в случае проблем:

docker volume prune -f

## Помимо MVP необходимо:

### Обязательно:

1. Retry и fallback при недоступности сервисов - добавить Polly
2. Rate limiting - базовое ограничение запросов
3. Реальные микросервисы (HTTP клиенты) - убрать хардкод

### Средней важности

4. Авторизация (JWT) - JWT Auth - базовая аутентификация
5. Circuit Breaker - через Polly

### Дополнительно

7. Мониторинг: Prometheus + Grafana
8. GraphQL - альтернативный эндпоинт
9. gRPC - оптимизация производительности
