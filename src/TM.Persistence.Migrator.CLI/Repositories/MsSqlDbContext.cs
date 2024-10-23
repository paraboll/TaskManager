using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TM.Persistence.Migrator.CLI.Repositories
{
    public class MsSqlDbContext : TMDbContext
    {
        private string _connectionString = "migration";

        public MsSqlDbContext() { } //Только для add-migration

        public MsSqlDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                _connectionString,
                x => x.MigrationsHistoryTable(
                    HistoryRepository.DefaultTableName, schemaName));
        }
    }
}
