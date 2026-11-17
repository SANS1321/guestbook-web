# GuestBook.Web

Гостевая книга на ASP.NET Core 8 (Razor Pages). Записи хранятся в Azure Table Storage,
фотографии — в Blob Storage, уведомления — в Queue Storage (локально — эмулятор Azurite).

## CI/CD

При каждом `git push` в ветку `main` GitHub Actions выполняет workflow
`.github/workflows/deploy.yml`:

1. **build** — на облачном runner `ubuntu-latest` проект собирается командой `dotnet publish`,
   результат сохраняется как артефакт;
2. **deploy** — self-hosted runner на сервере скачивает артефакт, останавливает старую версию,
   копирует новую в `C:\GuestBookSite` и запускает сайт на `http://localhost:8080`.

Строка подключения к хранилищу передаётся приложению из секрета `GUESTBOOK_STORAGE_CONNECTION`.
