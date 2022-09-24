To run a migration

dotnet ef migrations add UpdateDatabaseStructure --context ConstrutoraViverSA.Infrastructure.ApplicationContext -o 'Migrations'

To update the database
dotnet ef database update