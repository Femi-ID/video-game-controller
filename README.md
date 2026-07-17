# Video Game Controller

A CRUD REST API for cataloguing video game characters, built with ASP.NET Core and backed by SQL Server via Entity Framework Core. Each character has a name, the game it belongs to, and a role (Hero, Villain, Prince, Princess, or NPC). The project follows a service-layer architecture with DTOs, extension-method mappers, and partial-update semantics on `PUT`.

## Tech Stack

- **Framework:** .NET 10 / ASP.NET Core Web API
- **Database:** SQL Server via Entity Framework Core
- **API Docs:** OpenAPI (built-in) + Scalar UI
- **Serialization:** System.Text.Json with `JsonStringEnumConverter` so enums are sent/received as strings

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or LocalDB / Developer Edition)
- EF Core CLI: `dotnet tool install --global dotnet-ef`

## Setup

### 1. Clone and restore

```bash
git clone https://github.com/Femi-ID/video-game-controller.git
cd video-game-controller
dotnet restore
```

### 2. Configure the connection string

The connection string is not checked into source. Set it via .NET User Secrets for local development:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=.\SQLEXPRESS;Database=VideoGameAPI;Trusted_Connection=True;TrustServerCertificate=True"
```

Adjust the connection string if you're using LocalDB (`(localdb)\MSSQLLocalDB`) or a different SQL Server instance.

### 3. Apply migrations

Creates the database and the `Characters` table.

```bash
dotnet ef database update
```

### 4. Run

```bash
dotnet watch run
```

The API will be available at `http://localhost:5277` (HTTPS on `https://localhost:7108`). The Scalar API reference UI is at `http://localhost:5277/scalar/v1`.

## API Endpoints

| Method | Route | Description |
|---|---|---|
| GET | `/api/character` | List all characters |
| GET | `/api/character/{id}` | Get a single character by id |
| POST | `/api/character` | Create a new character |
| PUT | `/api/character/{id}` | Partially update a character |
| DELETE | `/api/character/{id}` | Delete a character |

### Request body — `POST /api/character`

```json
{
  "name": "Mario",
  "game": "Super Mario Bros",
  "role": "Hero"
}
```

`role` accepts one of: `Hero`, `Villain`, `Prince`, `Princess`, `NPC`.

### Request body — `PUT /api/character/{id}`

All fields are optional. Fields you omit (or send as `null`) are left untouched on the existing record — only the fields you include get overwritten.

```json
{
  "game": "Super Mario Odyssey"
}
```

## Design Notes

- **`GetAllCharactersAsync` and `GetCharacterByIdAsync`** use EF Core projection (`.Select(...)`) so only the DTO columns are pulled from SQL, and the resulting entities aren't tracked. The projection lives on `IQueryable<Character>` as an extension method (`ProjectToResponseDto`) in `CharacterMapper` so the shape is defined in one place.
- **`UpdateCharacterAsync`** fetches the tracked entity and mutates only the properties the client actually sent. `Name`/`Game` are `string?` and `Role` is `CharacterRole?` in `UpdateCharacterDto` so that "field not sent" is distinguishable from "field explicitly cleared."
- **`ICharacterService`** is registered scoped in `Program.cs`; the controller depends on the interface, not the concrete class, so the service can be swapped or mocked without touching the controller.

## Project Structure

```
.
├── Controllers/       # HTTP endpoints (CharacterController)
├── Data/              # AppDBContext
├── Dtos/              # Request/response DTOs
├── Enums/             # CharacterRole
├── Interfaces/        # ICharacterService
├── Mappers/           # Entity <-> DTO extension methods + projections
├── Migrations/        # EF Core migrations
├── Models/            # Character entity
├── Services/          # CharacterService implementation
└── Program.cs         # Composition root
```

## License

MIT