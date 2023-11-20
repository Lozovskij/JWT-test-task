# JWT-test-task
## Описание
Приложение офисной поддержки, всключающее 
- логин и регистрацию (UI - React)
- генерацию JWT и Refresh токенов (.NET Web API)
- элементы CRUD (бд - PostgreSQL)

Паттерны:
- улучшенная N-layer архитектура
- Repository
- Unit Of Work
- Mediatr
  
Библиотеки
- BCrypt.Net-Next
- Entity Framework Core
- MediatR

## Как запустить
Бэкенд
- указать строку подключения к бд
- сбилдить солюшн
- в PM консоли для `DataAccess` проекта выполнить команду `update-database`

Фронтенд
```
npm i
npm run dev
```
## Скриншоты
