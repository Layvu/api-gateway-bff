# API Gateway с агрегацией данных и кэшированием

## Запуск

docker-compose up --build -d

## Проверка

API Gateway: http://localhost:8080/api/profile/123

Seq logs: http://localhost:5341 (логин/пароль для первого входа: admin/Admin123!)

Redis: работает на порту 6379

### Основной сценарий (Gateway)

Агрегированный ответ (Пользователь + Заказы + Товары):

- http://localhost:8080/api/profile/1

### Прямой доступ к микросервисам (для отладки)

- Users: http://localhost:5001/api/users/1
- Products: http://localhost:5002/api/products
- Orders: http://localhost:5003/api/orders/user/1

## Для полного сброса докер томов в случае проблем:

docker volume prune -f

## Помимо MVP необходимо:

### Обязательно:

1. Retry и fallback при недоступности сервисов - добавить Polly - сделано
2. Rate limiting - базовое ограничение запросов - сделано
3. Реальные микросервисы (HTTP клиенты) - сделано

### Средней важности

4. Авторизация (JWT) - JWT Auth - базовая аутентификация
5. Circuit Breaker - через Polly

### Дополнительно

7. Мониторинг: Prometheus + Grafana
8. GraphQL - альтернативный эндпоинт
9. gRPC - оптимизация производительности
