using System;
using System.Threading;
using Microsoft.Extensions.CommandLineUtils;
using NLog;
using TM.Persistence.Migrator.CLI.Services;

namespace TM.Persistence.Migrator.CLI.Commands
{
    public class MigrationCommand
    {
        private static ILogger _log;
        private static TMDbContext _dbContext;

        public static void Register(CommandLineApplication app, ILogger log, TMDbContext dbContext)
        {
            _log = log;
            _dbContext = dbContext;

            app.Command("migration", Setup);
        }
        private static void Setup(CommandLineApplication command)
        {
            command.HelpOption("-?|-h|--help");
            command.Description = "Синхронизация ресурсов.";

            command.OnExecute(() =>
            {
                _log.Info($"Начало миграция данных. Время: {DateTime.Now}");

                var migrationService = new MigrationService(_log, _dbContext);
                migrationService.ApplyMigrationAsync(CancellationToken.None).Wait();

                _log.Info($"Миграция данных завершена. Время: {DateTime.Now}");

                return 0;
            });
        }
    }
}
