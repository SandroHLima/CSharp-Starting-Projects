# C# Starter Projects

A collection of beginner-friendly C# projects, starting with a console-based Todo List Manager. Each project will live in its own folder and build on practical C# concepts.

## Projects

1. **Todo List Manager** — CRUD, validation, priorities, and local JSON persistence (`TodoListManager/`)
2. **Standard Calculator** — arithmetic, numeric input, and error handling
3. **Weather App** — HTTP APIs, JSON, and refresh behavior
4. **Tic-Tac-Toe Game** — game state, two-player play, and a computer opponent
5. **Expense Tracker** — CRUD, filtering, summaries, and local storage
6. **Address Book** — contact management, search, sorting, and import/export
7. **Simple Chat App** — networking, authentication, and real-time messaging
8. **Quiz App** — questions, scoring, randomization, and timers
9. **Recipe Manager** — recipe CRUD, categories, and search
10. **Blogging Platform** — posts, authentication, comments, and pagination

The original feature notes are in [info.txt](info.txt).

## Getting started

Requires the .NET 10 SDK. To run the first project:

```powershell
dotnet run --project .\TodoListManager\TodoListManager.csproj
```

Todo data is saved locally under the current user's application data folder. It is not synced between devices; cross-device access will require a backend and authentication.
