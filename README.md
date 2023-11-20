# JWT-test-task
## Описание
Приложение для получения запросов от офисных сотрудников для их дальнейшей поддержки.

Приложение включает:
- логин и регистрацию (UI - React)
- генерацию JWT и Refresh токенов (.NET Web API)
- элементы CRUD (бд - PostgreSQL)

Паттерны:
- улучшенная N-layer архитектура
- Repository
- Unit Of Work
- Mediator
  
Библиотеки:
- BCrypt.Net-Next
- Entity Framework Core
- MediatR

## Как запустить
Бэкенд
- указать строку подключения к бд в `appsettings.json`
- в PM консоли выполнить команду `update-database`

Фронтенд
```
npm i
npm run dev
```
## Скриншоты
![localhost_5173_home](https://github.com/Lozovskij/JWT-test-task/assets/56762093/4e561f39-371a-4f31-bdfd-156ef07f7a98)
![localhost_5173_home (1)](https://github.com/Lozovskij/JWT-test-task/assets/56762093/130d86aa-5dbb-46c5-af1e-f4f01a610f22)
![localhost_5173_home (2)](https://github.com/Lozovskij/JWT-test-task/assets/56762093/fcc16ea0-b998-4f57-8af7-15de4a9123ce)
![localhost_5173_home (4)](https://github.com/Lozovskij/JWT-test-task/assets/56762093/9ae8a81f-1fae-4f39-baf5-70bb9cf4f5d4)
![localhost_5173_home (5)](https://github.com/Lozovskij/JWT-test-task/assets/56762093/a40769d6-6441-424d-8f23-ca8bbc3c77b9)
