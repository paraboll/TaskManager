using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.Application.Factories;
using TM.Application.Interfaces;
using TM.Domain.Entities;

namespace TM.Application.Repositoryies
{
    public class EmployeeRepositoryDecorator : IEmployeeRepository
    {
        private const string testLogin = "Login0";
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeRepositoryDecorator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            if(_employeeRepository.GetByLoginAsync(testLogin).Result == null)
                _employeeRepository.AddsAsync(EmployeeFactory.CreateEmployees().ToList());
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            return await _employeeRepository.AddAsync(employee);
        }
        public async Task AddsAsync(List<Employee> employees)
        {
            await _employeeRepository.AddsAsync(employees);
        }

        public async Task<Employee> EditAsync(Employee employee)
        {
            return await _employeeRepository.EditAsync(employee);
        }


        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }
        public async Task<Employee> GetByLoginAsync(string login)
        {
            return await _employeeRepository.GetByLoginAsync(login);
        }
        public async Task<IEnumerable<Employee>> SearchByLoginAsync(string login)
        {
            return await _employeeRepository.SearchByLoginAsync(login);
        }


        public async Task<Employee> DeleteAsync(string login)
        {
            return await _employeeRepository.DeleteAsync(login);
        }
        public async Task<Employee> LockAsync(string login)
        {
             return await _employeeRepository.LockAsync(login);
        }
        public async Task<Employee> UnlockAsync(string login)
        {
             return await _employeeRepository.UnlockAsync(login);
        }
    }
}
