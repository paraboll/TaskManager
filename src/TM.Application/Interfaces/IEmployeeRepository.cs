using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Domain.Entities;

namespace TM.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddAsync(Employee employee);
        Task AddsAsync(List<Employee> employees);
        Task<Employee> EditAsync(Employee employee);

        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByLoginAsync(string login);
        Task<IEnumerable<Employee>> SearchByLoginAsync(string login);

        Task<Employee> LockAsync(string login);
        Task<Employee> UnlockAsync(string login);
        Task<Employee> DeleteAsync(string login);
    }
}
