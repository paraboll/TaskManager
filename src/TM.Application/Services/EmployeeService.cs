using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Application.Interfaces;
using TM.Domain.Entities;
using TM.Domain.Extensions;
using Microsoft.Extensions.Logging;

namespace TM.Application.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly ILogger<IEmployeeService> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(ILogger<IEmployeeService> logger, 
            IEmployeeRepository employeeRepository)
        {
            _logger = logger;

            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            employee.ToCreateValid();
            employee.Status = EmployeeStatus.Lock;

            return await _employeeRepository.AddAsync(employee);
        }
        public async Task<Employee> EditAsync(Employee employee)
        {
            employee.ToEditValid();

            return await _employeeRepository.EditAsync(employee);
        }

        public async Task<IEnumerable<Employee>> SearchByLoginAsync(string login)
        {
            if (string.IsNullOrEmpty(login))
                return await _employeeRepository.GetAllAsync();

            return await _employeeRepository.SearchByLoginAsync(login);
        }
        public async Task<Employee> GetByLoginAsync(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException("login не должен быть null");

            return await _employeeRepository.GetByLoginAsync(login);
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee> LockAsync(string login)
        {
            var employee = await _employeeRepository.GetByLoginAsync(login);
            if (employee == null)
            {
                throw new Exception($"EmployeeService. Cотрудник {login} не найден.");
            }

            if (employee.Status != EmployeeStatus.Work)
            {
                throw new Exception($"Не удалось заблокировать сотрудника {employee.Login}:{employee.Status}");
            }

            return await _employeeRepository.LockAsync(login);
        }
        public async Task<Employee> UnlockAsync(string login)
        {
            var employee = await _employeeRepository.GetByLoginAsync(login);
            if (employee == null)
            {
                throw new Exception($"EmployeeService. Cотрудник {login} не найден.");
            }

            if (employee.Status != EmployeeStatus.Lock)
            {
                throw new Exception($"Не удалось разблокировать сотрудника {employee.Login}:{employee.Status}");
            }

            return await _employeeRepository.UnlockAsync(login);
        }
        public async Task<Employee> DeleteAsync(string login)
        {
            var employee = await _employeeRepository.GetByLoginAsync(login);
            if (employee == null)
            {
                throw new Exception($"EmployeeService. Cотрудник {login} не найден.");
            }

            if (employee.Status != EmployeeStatus.Lock)
            {
                throw new Exception($"Не удалось удалить сотрудника {employee.Login}:{employee.Status}");
            }

            return await _employeeRepository.DeleteAsync(login);
        }
        public async Task<Employee> ReinstateAsync(string login)
        {
            var employee = await _employeeRepository.GetByLoginAsync(login);
            if (employee == null)
            {
                throw new Exception($"EmployeeService. Cотрудник {login} не найден.");
            }

            if (employee.Status != EmployeeStatus.Delete)
            {
                throw new Exception($"Не удалось восстановить сотрудника {employee.Login}:{employee.Status}");
            }

            return await _employeeRepository.LockAsync(login);
        }
    }
}
