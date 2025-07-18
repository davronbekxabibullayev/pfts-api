# pfts-api
Personal Finance Tracker System

CLI
```
dotnet ef migrations add "SampleMigration" --project Pfts.Infrastructure --startup-project Pfts.Api --output-dir Persistance\EntityFramework\Migrations
```
Package-Mananger Console
```
Add-Migration InitialMigration -Project Pfts.Infrastructure -OutputDir Persistance\EntityFramework\Migrations
```
