# GuestBook.Web

Гостевая книга на ASP.NET Core 8 (Razor Pages). Записи хранятся в Azure Table Storage,
фотографии — в Blob Storage, уведомления — в Queue Storage (локально — эмулятор Azurite).

## CI/CD

При каждом `git push` в ветку `main` GitHub Actions выполняет workflow
`.github/workflows/deploy.yml` (задание `build-and-deploy` на self-hosted runner сервера):

1. `actions/checkout` — получение кода;
2. `actions/setup-dotnet` — установка .NET 8 SDK;
3. `dotnet publish` — сборка релизной версии;
4. деплой — остановка старой версии, копирование новой в `C:\GuestBookSite`
   и запуск сайта на `http://localhost:8080`, затем проверка, что сайт отвечает.

Строка подключения к хранилищу передаётся приложению из секрета `GUESTBOOK_STORAGE_CONNECTION`.
