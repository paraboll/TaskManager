using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Application.Factories;
using TM.Application.Interfaces;
using TM.Domain.Entities;

namespace TM.Application.Repositoryies
{
    public class BuisnessTaskRepositoryDecorator: IBuisnessTaskRepository
    {
        private const string testExternalId = "ExternalId0";
        private readonly IBuisnessTaskRepository _buisnessTaskRepository;

        public BuisnessTaskRepositoryDecorator(IBuisnessTaskRepository buisnessTaskRepository)
        {
            _buisnessTaskRepository = buisnessTaskRepository;

            if (_buisnessTaskRepository.GetByExternalIdIdAsync(testExternalId).Result == null)
                _buisnessTaskRepository.AddsAsync(BuisnessTaskFactory.CreateBuisnessTask().ToList());
        }

        public async Task<BuisnessTask> AddAsync(BuisnessTask buisnessTask)
        {
            return await _buisnessTaskRepository.AddAsync(buisnessTask);
        }

        public async Task AddsAsync(List<BuisnessTask> buisnessTasks)
        {
            await _buisnessTaskRepository.AddsAsync(buisnessTasks);
        }

        public async Task<BuisnessTask> AddTaskCommentAsync(TaskComment taskComment)
        {
            return await _buisnessTaskRepository.AddTaskCommentAsync(taskComment);
        }

        public async Task<BuisnessTask> EditAsync(BuisnessTask buisnessTask)
        {
            return await _buisnessTaskRepository.EditAsync(buisnessTask);
        }

        public async Task<IEnumerable<BuisnessTask>> GetAllAsync()
        {
            return await _buisnessTaskRepository.GetAllAsync();
        }

        public async Task<BuisnessTask> GetByExternalIdIdAsync(string extensionId)
        {
            return await _buisnessTaskRepository.GetByExternalIdIdAsync(extensionId);
        }

        public async Task<BuisnessTask> RemoveTaskCommentAsync(TaskComment taskComment)
        {
            return await _buisnessTaskRepository.RemoveTaskCommentAsync(taskComment);
        }

        public async Task<IEnumerable<BuisnessTask>> SearchByNameAsync(string name)
        {
            return await _buisnessTaskRepository.SearchByNameAsync(name);
        }
    }
}
