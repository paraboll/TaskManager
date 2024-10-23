Add-Migration "Имя миграции" -Context "Имя констекста" -OutputDir "куда положить"

Add-Migration InitialDbMsSql -Context MsSqlDbContext -OutputDir Migrations\MsSql
Add-Migration InitialDbPostgres -Context PostgresDbContext -OutputDir Migrations\PostgreSql