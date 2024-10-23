using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TM.Persistence.Migrator.CLI.Repositories
{
    public class PostgresDbContext : TMDbContext
    {
        private string _connectionString = "migration";

        public PostgresDbContext() { } //Только для add-migration

        public PostgresDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(
                _connectionString,
                x => x.MigrationsHistoryTable(
                    HistoryRepository.DefaultTableName, schemaName));
        }
    }
}
