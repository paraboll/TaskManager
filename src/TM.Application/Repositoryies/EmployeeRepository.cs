using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.Application.Interfaces;
using TM.Domain.Entities;
using TM.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TM.Application.Repositoryies
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ILogger<TMDbContext> _log;
        private readonly IDbContextFactory<TMDbContext> _dbFactory;
        public EmployeeRepository(ILogger<TMDbContext> log,
            IDbContextFactory<TMDbContext> dbFactory)
        {   
            _log = log;
            _dbFactory = dbFactory;
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var newEmployee = await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();
                return newEmployee.Entity;
            }
        }

        //TODO: пересмотреть механизм обновления.
        public async Task<Employee> EditAsync(Employee employee)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var editEmployee = await context.Employees.FirstOrDefaultAsync(_ => _.Login == employee.Login);
                if (editEmployee == null)
                    throw new Exception($"Сотрудник {employee.Login} не найден.");

                var temp = editEmployee.Id;

                editEmployee = employee;
                editEmployee.Id = temp;

                await context.SaveChangesAsync();
                return editEmployee;
            }
        }

        public async Task AddsAsync(List<Employee> employees)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                try
                {
                    await context.Employees.AddRangeAsync(employees);
                    await context.SaveChangesAsync();
                }

                catch (Exception ex)
                {
                    _log.LogError($"error: {ex.Message}");
                }
            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.Employees
                    .AsNoTracking()
                    .ToListAsync();
            }
        }
        public async Task<Employee> GetByLoginAsync(string login)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.Employees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(_ => _.Login == login);
            }
        }

        public async Task<IEnumerable<Employee>> SearchByLoginAsync(string login)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.Employees
                    .AsNoTracking()
                    .Where(_ => _.Login.Contains(login))
                    .ToListAsync();
            }
        }

        public async Task<Employee> LockAsync(string login)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var employee = await context.Employees
                    .FirstOrDefaultAsync(_ => _.Login == login);

                employee.Status = EmployeeStatus.Lock;

                await context.SaveChangesAsync();

                return employee;
            }
        }
        public async Task<Employee> UnlockAsync(string login)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var employee = await context.Employees
                    .FirstOrDefaultAsync(_ => _.Login == login);

                employee.Status = EmployeeStatus.Work;

                await context.SaveChangesAsync();

                return employee;
            }
        }
        public async Task<Employee> DeleteAsync(string login)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var employee = await context.Employees
                    .FirstOrDefaultAsync(_ => _.Login == login);

                employee.Status = EmployeeStatus.Delete;

                await context.SaveChangesAsync();

                return employee;
            }
        }
    }
}
