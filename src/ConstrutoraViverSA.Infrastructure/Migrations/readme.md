# To run a migration

dotnet ef migrations add InitMigration --context ConstrutoraViverSA.Infrastructure.ApplicationContext -o 'Migrations'

# To update the database

dotnet ef database update