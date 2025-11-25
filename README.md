# Personal Growth Tracker

A compact full-stack application for tracking daily mood and personal
progress, built with **.NET 9 Web API**, **Angular 19**, and
**SQLite**.\
The project is intentionally kept focused while demonstrating clear
architectural structure, separation of concerns, and several
production‑grade patterns (caching, persistence, and deployment
readiness).

------------------------------------------------------------------------

## Preview

<img width="450" height="734" alt="Image" src="https://github.com/user-attachments/assets/59304715-44cf-4f2d-826a-aace4b9e4040" />

------------------------------------------------------------------------

## Features

### Backend (.NET 9 API)

-   Minimal and explicit API surface
-   Domain layer with entities and repository abstractions
-   SQLite persistence via EF Core
-   Automatic migrations and initial data seeding
-   Endpoints:
    -   `GET /api/mood` -- list recent mood entries
    -   `GET /api/mood/{id}` -- fetch a single entry
    -   `POST /api/mood` -- create a new entry
-   Health endpoint: `GET /health`
-   Explicit HTTP caching for read/write operations
-   Global CORS configuration for browser clients

### Frontend (Angular 19)

-   Lightweight SPA with modular component structure
-   Form for adding daily mood (1--10) with optional note
-   Custom-styled numeric input with keyboard and inline controls
-   Scrollable list of historical entries
-   Basic handling of loading and error states
-   Local development proxy for API integration

### Persistence

-   SQLite used as a lightweight relational store
-   EF Core migrations fully manage schema evolution
-   Seed data applied on first run when the table is empty

------------------------------------------------------------------------

## Project Structure

``` text
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

------------------------------------------------------------------------

## Running Locally

### 1. Backend API

From the repository root:

``` bash
dotnet run --project PersonalGrowthTracker.Api
```

This will:

-   Apply EF Core migrations
-   Create the local SQLite database file:

``` text
personal_growth.db
```

The API runs on the HTTPS port defined in *launchSettings.json*.

------------------------------------------------------------------------

### 2. Frontend (Angular)

From the repository root:

``` bash
cd personal-growth-tracker-web
npm install
npm start
```

The application will be available at:

    http://localhost:4200

Angular dev server proxies API calls using `proxy.conf.json`:

``` jsonc
{
  "/api": {
    "target": "https://localhost:7021",
    "secure": false,
    "changeOrigin": true
  }
}
```

------------------------------------------------------------------------

## Implementation Notes

-   Read endpoints include short-lived `Cache-Control` headers
-   Write endpoints are marked `no-store`
-   EF Core migrations run automatically on startup
-   Seed data is applied only when no mood entries exist
-   Repository implementation uses EF Core (`EfMoodRepository`)
-   A single global CORS policy applies to all API routes

The codebase is intentionally compact and optimized for clarity, while
still reflecting real-world concerns such as caching, persistence, and
API-SPA integration.
