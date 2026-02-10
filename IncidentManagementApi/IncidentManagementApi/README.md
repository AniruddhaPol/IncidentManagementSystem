# IncidentManagementApi

Minimal README with setup and run instructions for the IncidentManagementApi (.NET 7).

## Prerequisites

- .NET 7 SDK installed (dotnet --version should show a 7.x version).
- (Optional) Azure Storage account for blob storage.
- (Optional) Visual Studio 2022 or newer for IDE experience.

## Configuration

The API requires an Azure Blob Storage connection string and a container name. You can provide these via `appsettings.json` or environment variables.


## What this project includes

- Used Entity Framework Core with SQLite database (local file `incidents.db`) configured via `UseSqlite`.
- Swagger UI enabled for exploring the API.
- CORS policy named `AllowReact` configured in `Program.cs` (adjust the allowed origin as needed).

## Deployment 
- Deployed to Azure App Services and can be accessblie from below URL's:
- API URL : https://incident-api-app-gvbfaycncbefbuhh.centralus-01.azurewebsites.net/swagger/index.html
- REACT App URL : https://incident-react-app-efadh9fgfdatdahv.eastus-01.azurewebsites.net/