# Sports Odds Tracker

A minimal .NET 9 API with CI/CD pipeline demo.

## Features

- **GET /api/odds**  
  Returns all sports odds.

- **GET /api/odds/{id}**  
  Returns odds for a specific game by ID.

- **POST /api/odds**  
  Upserts a game odds record with validation.  
  Body format:  
  {  
  &nbsp;&nbsp;&nbsp;&nbsp;"gameId": string,  
  &nbsp;&nbsp;&nbsp;&nbsp;"homeTeam": string,  
  &nbsp;&nbsp;&nbsp;&nbsp;"awayTeam": string,  
  &nbsp;&nbsp;&nbsp;&nbsp;"spread": decimal,  
  &nbsp;&nbsp;&nbsp;&nbsp;"overUnder": decimal  
  }

## Local Run Instructions
dotnet build  
dotnet test  
dotnet run --project Api/Api.csproj

## Testing Notes

- Unit tests cover repository logic.
- Integration tests (optional) cover API endpoints using WebApplicationFactory.

## Deployment

_Deployment link will be added in a later stage._
