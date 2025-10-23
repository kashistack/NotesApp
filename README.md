
# NotesApp (ASP.NET Core 8 MVC) - Clean Architecture (Minimal)

This is a minimal, modular Notes application built with ASP.NET Core 8 MVC, EF Core (Code-First), and a Dockerized SQL Server (for the database). The UI is plain HTML (no Bootstrap) and supports:
- Create notes (title, content, priority)
- Inline edit (edit form shown per note)
- Delete notes
- Notes stored via EF Core to SQL Server
- Notes displayed in reverse chronological order (newest first)
- Color-coding by priority: Low (green), Medium (yellow), High (red)

## Quick steps (Windows / CMD)

1. Extract the zip and open a terminal in the `NotesApp` folder.
2. Start SQL Server Docker container (docker-compose is provided):
   ```cmd
   docker compose up -d
   ```
   This runs SQL Server and maps port 1433 -> host 1433. SA password is `Your@Password123` (change if desired).

3. Restore tools if needed:
   ```cmd
   dotnet tool install --global dotnet-ef --version 8.*
   ```

4. Restore and build:
   ```cmd
   dotnet restore
   dotnet build
   ```

5. Create the database (EF Core migrations are not included; use `EnsureCreated` at runtime or run commands):
   ```cmd
   dotnet ef database update -p Notes.Infrastructure -s Notes.Web
   ```
   If `dotnet ef` is not configured, the app will create the DB automatically on startup using `EnsureCreated`.

6. Run the Web app:
   ```cmd
   dotnet run --project Notes.Web
   ```
   Open: http://localhost:5000  (or https://localhost:5001)

## Project layout

- Notes.Domain/         (entities, enums)
- Notes.Application/    (service interfaces, DTOs)
- Notes.Infrastructure/ (EF Core DbContext)
- Notes.Web/            (ASP.NET Core MVC site)

## Docker (SQL Server)

`docker-compose.yml` provides a service `sqlserver` using `mcr.microsoft.com/mssql/server:2022-latest`:
- SA password: `Your@Password123`
- Port: `1433:1433`

Run:
```bash
docker compose up -d
```

## Notes

- This is a minimal example meant to be used as a starting point. Add migrations, tests, authentication, validations and production hardening when deploying.
