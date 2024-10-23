using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;
using TM.Domain.Entities;

namespace TM.Persistence.Migrator.CLI.Services
{
    public class GenerateDataService
    {
        private readonly ILogger _logger;
        private readonly TMDbContext _applicationContext;

        public GenerateDataService(ILogger logger, TMDbContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }

        public async Task GenerateData(CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Выполнение команды генерации данных.");

                var pendingMigrations = await _applicationContext.Database.GetPendingMigrationsAsync(cancellationToken);

                if (pendingMigrations.Any())
                {
                    Console.WriteLine($"Не все изменения применены, проведите миграцию данных.");
                    return;
                }

                await EmployeeGenerateAsync();
                await WithBuisnessTasksAsync();
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

        private async Task EmployeeGenerateAsync(int count = 100)
        {

            var employees = Enumerable.Range(0, count).Select(i =>
            {
                return new Employee()
                {
                    //Id = i,
                    Login = $"Login{i}",
                    FirstName = $"FirstName{i}",
                    MiddleName = $"MiddleName{i}",
                    LastName = $"LastName{i}",
                    Password = $"Password{i}",
                    Status = (EmployeeStatus)(i % 3)
                };
            });

            await _applicationContext.Employees.AddRangeAsync(employees);
            await _applicationContext.SaveChangesAsync();
        }

        private async Task WithBuisnessTasksAsync(int countBT = 100, int countTC = 5)
        {
            var buisnessTasks = Enumerable.Range(0, countBT).Select(i =>
            {
                return new BuisnessTask()
                {
                    ExternalId = $"ExternalId{i}",
                    Name = $"Name{i}",
                    Description = $"Description{i}",
                    AuthorId = (i % 5) + 1,
                    EmployeeId = (i % 5) + 1,
                    Status = (BuisnessTaskStatus)(i % 5),
                    TaskComments = Enumerable.Range(0, countTC).Select(j =>
                    {
                        return new TaskComment()
                        {
                            ExternalId = Guid.NewGuid().ToString(),
                            Time = DateTime.Now,
                            Text = $"Text{j}",
                            BuisnessTaskId = j + 1,
                            AuthorId = (j % 5) + 1
                        };
                    }).ToList()
                };
            });

            await _applicationContext.BuisnessTasks.AddRangeAsync(buisnessTasks);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
