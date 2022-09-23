To run a migration

C:\Studyspace\construtora-viver-sa\src\ConstrutoraViverSA.Infraestrutura> dotnet ef migrations add UpdateDatabaseStructure --context ConstrutoraViverSA.Infraestrutura.ApplicationCon
text -o 'Migrations'

To update the database
dotnet ef database update