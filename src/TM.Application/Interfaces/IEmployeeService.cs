using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Domain.Entities;

namespace TM.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee> EditAsync(Employee employee);

        Task<IEnumerable<Employee>> SearchByLoginAsync(string login);
        Task<Employee> GetByLoginAsync(string login);
        Task<IEnumerable<Employee>> GetAllAsync();


        Task<Employee> LockAsync(string login);
        Task<Employee> UnlockAsync(string login);
        Task<Employee> DeleteAsync(string login);
        Task<Employee> ReinstateAsync(string login);
    }
}
