# Personal Growth Tracker

A small full-stack application for tracking daily mood and personal progress.  
Built with **.NET 9 Web API**, **Angular 19**, and **SQLite**.

The goal of the project is to stay small and focused, but still show clear structure, separation of concerns, and a few practical production-style patterns (caching, persistence, and deployment).

---

## Preview

<img width="450" height="734" alt="Image" src="https://github.com/user-attachments/assets/59304715-44cf-4f2d-826a-aace4b9e4040" />

---

## Features

### Backend (.NET 9 API)

- Minimal, focused API surface
- Domain layer for entities and repository abstractions
- SQLite persistence using EF Core
- Automatic migrations and initial data seeding on startup
- Endpoints:
  - `GET /api/mood` – list recent mood entries
  - `GET /api/mood/{id}` – get a single entry
  - `POST /api/mood` – create a new entry
- Health endpoint: `GET /health`
- Explicit HTTP caching on read/write operations
- CORS enabled for browser clients

### Frontend (Angular 19)

- Single-page mood tracking UI
- Form for adding daily mood (1–10) with optional note
- Custom-styled numeric input with inline controls
- Scrollable list of recent entries
- Basic loading and error states
- Local dev proxy for API calls

### Persistence

- SQLite used as a lightweight relational store
- EF Core migrations manage the schema
- Seed data added on first run to avoid an empty UI

---

## Project structure

```text
/PersonalGrowthTracker.Api
  /Domain
    /Entities
    /Repositories
  /Infrastructure
    /Data
    /Repositories
  /Controllers
  Program.cs

/personal-growth-tracker-web
  /src/app
    /mood-tracker
  proxy.conf.json
  angular.json
```

---

## Running locally

### 1. Backend API

From the repository root:

```bash
dotnet run --project PersonalGrowthTracker.Api
```

This will:

- apply EF Core migrations
- create a local SQLite database file:

```text
personal_growth.db
```

The API will be available on the port shown in the console (usually `https://localhost:xxxx`).

### 2. Frontend (Angular)

From the repository root:

```bash
cd personal-growth-tracker-web
npm install
npm start
```

The app will be available at:

```text
http://localhost:4200
```

Angular dev server proxies API calls to the backend via `proxy.conf.json`:

```jsonc
{
  "/api": {
    "target": "https://localhost:7021", 
    "secure": false,
    "changeOrigin": true
  }
}
```


---

## Implementation notes

- Read endpoints set short-lived `Cache-Control` headers
- Write endpoints are marked as `no-store`
- EF Core migrations are applied automatically on startup
- Seed data is applied only when the mood table is empty
- Repository implementation uses EF Core (`EfMoodRepository`)
- CORS policy is configured once and applied globally to API routes

This is intentionally a small codebase, meant to be easy to read end-to-end while still touching on real-world concerns like caching, persistence, and browser-to-API integration.
