using NLog;
using System;
using System.Threading;
using Microsoft.Extensions.CommandLineUtils;
using TM.Persistence.Migrator.CLI.Services;

namespace TM.Persistence.Migrator.CLI.Commands
{
    internal static class GenerateCommand
    {
        private static ILogger _log;
        private static TMDbContext _dbContext;

        public static void Register(CommandLineApplication app, ILogger log, TMDbContext dbContext)
        {
            _log = log;
            _dbContext = dbContext;

            app.Command("generate", Setup);
        }

        private static void Setup(CommandLineApplication command)
        {
            command.HelpOption("-?|-h|--help");
            command.Description = "Заполнение БД тестовыми данными.";

            command.OnExecute(() =>
            {
                _log.Info($"Начало генерации данных. Время: {DateTime.Now}");

                var generateDataService = new GenerateDataService(_log, _dbContext);
                generateDataService.GenerateData(CancellationToken.None).Wait();

                _log.Info($"Генерация данных завершена. Время: {DateTime.Now}");

                return 0;
            });
        }
    }
}
