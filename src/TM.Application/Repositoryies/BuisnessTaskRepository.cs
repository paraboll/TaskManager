using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TM.Application.Interfaces;
using TM.Domain.Entities;
using TM.Persistence;

namespace TM.Application.Repositoryies
{
    public class BuisnessTaskRepository : IBuisnessTaskRepository
    {
        private readonly ILogger<BuisnessTaskRepository> _log;
        private readonly IDbContextFactory<TMDbContext> _dbFactory;
        public BuisnessTaskRepository(ILogger<BuisnessTaskRepository > log,
            IDbContextFactory<TMDbContext> dbFactory)
        {
            _log = log;
            _dbFactory = dbFactory;
        }

        public async Task<BuisnessTask> AddAsync(BuisnessTask buisnessTask)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var newBuisnessTask = await context.BuisnessTasks.AddAsync(buisnessTask);
                await context.SaveChangesAsync();
                return newBuisnessTask.Entity;
            }
        }

        public async Task AddsAsync(List<BuisnessTask> buisnessTasks)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                try
                {
                    await context.BuisnessTasks.AddRangeAsync(buisnessTasks);
                    await context.SaveChangesAsync();
                }

                catch (Exception ex)
                {
                    _log.LogError($"error: {ex.Message}");
                }
            }
        }

        //TODO: пересмотреть механизм обновления.
        public async Task<BuisnessTask> EditAsync(BuisnessTask buisnessTask)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var editBuisnessTask = await context.BuisnessTasks.FirstOrDefaultAsync(_ => _.ExternalId == buisnessTask.ExternalId);
                if (editBuisnessTask == null)
                    throw new Exception($"Задача {editBuisnessTask.Name} не найдена.");

                var temp = editBuisnessTask.Id;

                editBuisnessTask = buisnessTask;
                editBuisnessTask.Id = temp;

                await context.SaveChangesAsync();
                return editBuisnessTask;
            }
        }

        public async Task<IEnumerable<BuisnessTask>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.BuisnessTasks
                    .AsNoTracking()
                    .Include(_ => _.Author)
                    .Include(_ => _.Employee)
                    .ToListAsync();
            }
        }

        public async Task<BuisnessTask> GetByExternalIdIdAsync(string externalId)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.BuisnessTasks
                    .AsNoTracking()
                    .Include(_ => _.Author)
                    .Include(_ => _.Employee)
                    .Include(_ => _.TaskComments)
                        .ThenInclude(_ => _.Author)
                    .FirstOrDefaultAsync(_ => _.ExternalId == externalId);
            }
        }

        public async Task<IEnumerable<BuisnessTask>> SearchByNameAsync(string name)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.BuisnessTasks
                    .AsNoTracking()
                    .Where(_ => _.Name.Contains(name))
                    .Include(_ => _.Author)
                    .Include(_ => _.Employee)
                    .ToListAsync();
            }
        }

        public async Task<BuisnessTask> AddTaskCommentAsync(TaskComment taskComment)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var buisnessTask = await context.BuisnessTasks
                    .Include(_ => _.Author)
                    .Include(_ => _.Employee)
                    .Include(_ => _.TaskComments)
                        .ThenInclude(_ => _.Author)
                    .FirstOrDefaultAsync(_ => _.ExternalId == taskComment.BuisnessTask.ExternalId);

                if (buisnessTask == null)
                    throw new Exception($"Не найдена задача с {taskComment.BuisnessTask.ExternalId}");

                buisnessTask.TaskComments.Add(taskComment);
                await context.SaveChangesAsync();
                
                return buisnessTask;
            }
        }

        public async Task<BuisnessTask> RemoveTaskCommentAsync(TaskComment taskComment)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var buisnessTask = await context.BuisnessTasks
                     .Include(_ => _.Author)
                     .Include(_ => _.Employee)
                     .Include(_ => _.TaskComments)
                         .ThenInclude(_ => _.Author)
                     .FirstOrDefaultAsync(_ => _.ExternalId == taskComment.BuisnessTask.ExternalId);

                if (buisnessTask == null)
                    throw new Exception($"Не найдена задача с {taskComment.BuisnessTask.ExternalId}");

                var removeTask = buisnessTask.TaskComments
                    .FirstOrDefault(_ => _.ExternalId == taskComment.ExternalId);

                if(removeTask == null)
                    throw new Exception($"Не найден коментарий с {taskComment.ExternalId}");

                buisnessTask.TaskComments.Remove(removeTask);
                await context.SaveChangesAsync();

                return buisnessTask;
            }
        }
    }
}
