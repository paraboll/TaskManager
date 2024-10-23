using System;
using TM.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TM.Domain.Entities;
using System.Collections.Generic;
using TM.Domain.Extensions;

namespace TM.Application.Services
{
    public class BuisnessTaskService: IBuisnessTaskService
    {
        private readonly ILogger<IBuisnessTaskService> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBuisnessTaskRepository _buisnessTaskRepository;

        public BuisnessTaskService(ILogger<IBuisnessTaskService> logger, 
            IEmployeeRepository employeeRepository,
            IBuisnessTaskRepository buisnessTaskRepository)
        {
            _logger = logger;

            _employeeRepository = employeeRepository;
            _buisnessTaskRepository = buisnessTaskRepository;
        }

        public async Task<BuisnessTask> CreateAsync(BuisnessTask buisnessTask)
        {
            //TODO: можно добавить сущность строителя и инкапсулировать эту логику в нем.
            buisnessTask.ToCreateValid();

            var author = await _employeeRepository.GetByLoginAsync(buisnessTask.Author.Login);

            if (author == null)
                throw new Exception($"Автор {buisnessTask.Author.Login} не найден.");
            if (!author.IsWork())
                throw new Exception($"Автор {author.Login}:{author.Status} не может создавать задачи");

            buisnessTask.AuthorId = author.Id;

            if (buisnessTask.Employee != null)
            {
                var employee = await _employeeRepository.GetByLoginAsync(buisnessTask.Employee.Login);
                if (author == null)
                    throw new Exception($"Сотрудник {buisnessTask.Employee.Login} не найден.");
                if (!employee.IsWork())
                    throw new Exception($"Сотрудник {author.Login}:{author.Status} не может выполнять задачи");

                buisnessTask.EmployeeId = employee.Id;
            }

            buisnessTask.Status = BuisnessTaskStatus.WaitingStart;
            buisnessTask.ExternalId = Guid.NewGuid().ToString();

            return await _buisnessTaskRepository.AddAsync(buisnessTask);
        }

        public async Task<BuisnessTask> EditAsync(BuisnessTask buisnessTask)
        {
            buisnessTask.ToEditValid();

            var author = await _employeeRepository.GetByLoginAsync(buisnessTask.Author.Login);

            if (author == null)
                throw new Exception($"Автор {buisnessTask.Author.Login} не найден.");
            if (!author.IsWork())
                throw new Exception($"Автор {author.Login}:{author.Status} не может создавать задачи");

            buisnessTask.AuthorId = author.Id;

            if (buisnessTask.Employee != null)
            {
                var employee = await _employeeRepository.GetByLoginAsync(buisnessTask.Employee.Login);
                if (author == null)
                    throw new Exception($"Сотрудник {buisnessTask.Employee.Login} не найден.");
                if (!employee.IsWork())
                    throw new Exception($"Сотрудник {author.Login}:{author.Status} не может выполнять задачи");

                buisnessTask.EmployeeId = employee.Id;
            }

            return await _buisnessTaskRepository.EditAsync(buisnessTask);
        }

        public async Task<IEnumerable<BuisnessTask>> GetAllAsync()
        {
             return await _buisnessTaskRepository.GetAllAsync();
        }

        public async Task<BuisnessTask> GetByExternalIdAsync(string extensionId)
        {
            if (string.IsNullOrEmpty(extensionId))
                throw new ArgumentNullException("login не должен быть null");

            return await _buisnessTaskRepository.GetByExternalIdIdAsync(extensionId);
        }

        public async Task<IEnumerable<BuisnessTask>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return await _buisnessTaskRepository.GetAllAsync();

            return await _buisnessTaskRepository.SearchByNameAsync(name);
        }

        public async Task<BuisnessTask> AddTaskCommentAsync(TaskComment taskComment)
        {
            taskComment.ToCreateValid();

            taskComment.ExternalId = Guid.NewGuid().ToString();
            return await _buisnessTaskRepository.AddTaskCommentAsync(taskComment);
        }

        public async Task<BuisnessTask> RemoveTaskCommentAsync(TaskComment taskComment)
        {
            taskComment.ToRemoveValid();

            return await _buisnessTaskRepository.RemoveTaskCommentAsync(taskComment);
        }
    }
}
