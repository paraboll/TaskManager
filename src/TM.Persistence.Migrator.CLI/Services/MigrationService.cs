using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace TM.Persistence.Migrator.CLI.Services
{
    public class MigrationService
    {
        private readonly ILogger _logger;
        private readonly TMDbContext _applicationContext;

        public MigrationService(ILogger logger, TMDbContext applicationContext)
        {
            _logger = logger;

            _applicationContext = applicationContext;
        }

        /// <summary>
        /// Применение всех миграций к БД соответствующего провайдера.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ApplyMigrationAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Выполнение команды Обновление схемы БД.");
                var pendingMigrations = await _applicationContext.Database.GetPendingMigrationsAsync(cancellationToken);

                if (!pendingMigrations.Any())
                {
                    _logger.Info($"Все изменения уже применены");
                    return;
                }

                var migrator = _applicationContext.GetInfrastructure().GetService<IMigrator>();
                foreach (var migration in pendingMigrations)
                {
                    _logger.Info("Применяется {Migration} ...", migration);
                    await migrator.MigrateAsync(migration, cancellationToken);
                }

                _logger.Info("Все изменения применены успешно");
                _logger.Info("Обновление схемы БД успешно завершено.");
            }
            catch (Exception exc)
            {
                var str = new StringBuilder();
                str.AppendLine($"Message: {exc.Message}");
                if (exc.InnerException != null)
                {
                    str.AppendLine($"\nInnerException: {exc.InnerException}");
                }

                str.AppendLine($"\nStackTrace: {exc.StackTrace}");

                _logger.Error($"При миграции произошла ошибка. {str.ToString()}");
                Console.WriteLine($"При миграции произошла ошибка. {str.ToString()}");
            }
        }
    }
}
