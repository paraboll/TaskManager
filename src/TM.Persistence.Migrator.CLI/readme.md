## Запуск миграции:
	TM.Persistence.Migrator.CLI.exe migration

### Поддреживаемые провайдеры бд
 - SQLServer
 - Postgres

### Пример config.json
```
{
  "DbConfig": {
    "Provider": "SQLServer",
    "ConnectionString": "Server=localhost;Database=helloappdb1;User Id=sa;Password=sa;TrustServerCertificate=false;"
  }
}
```