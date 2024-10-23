using System;

namespace TM.Persistence.Migrator.CLI.Repositories
{
    public static class ContextFactory
    {
        public static TMDbContext CreateContext(DbConfig dbConfig)
        {
            switch (dbConfig.Provider)
            {
                case DbProviders.SQLServer:
                    return new MsSqlDbContext(dbConfig.ConnectionString);
                case DbProviders.Postgres:
                    return new PostgresDbContext(dbConfig.ConnectionString);
                default:
                    throw new Exception($"Провайдер {dbConfig.Provider} не поддерживается.");
            }
        }
    }
}
