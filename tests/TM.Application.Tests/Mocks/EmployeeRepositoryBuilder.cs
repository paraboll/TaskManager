using System.Collections.Generic;
using Moq;
using TM.Application.Interfaces;
using TM.Domain.Entities;

namespace TM.Application.Tests.Mocks
{
    public class EmployeeRepositoryBuilder
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;

        public EmployeeRepositoryBuilder()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
        }

        public EmployeeRepositoryBuilder WithGetAllAsync(IEnumerable<Employee> employees)
        {
            _mockRepository
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(employees);
            return this;
        }

        public EmployeeRepositoryBuilder WithGetByLoginAsync(string login, Employee employee)
        {
            _mockRepository
                .Setup(r => r.GetByLoginAsync(login))
                .ReturnsAsync(employee);
            return this;
        }

        public EmployeeRepositoryBuilder WithSearchByLoginAsync(string login, IEnumerable<Employee> employees)
        {
            _mockRepository
                .Setup(r => r.SearchByLoginAsync(login))
                .ReturnsAsync(employees);
            return this;
        }

        public EmployeeRepositoryBuilder WithAddAsync(Employee newEmployee, Employee employee)
        {
            _mockRepository
                .Setup(r => r.AddAsync(It.Is<Employee>(e => e.Login == newEmployee.Login)))
                .ReturnsAsync(employee);
            return this;
        }

        public EmployeeRepositoryBuilder WithEditAsync(Employee newEmployee, Employee employee)
        {
            _mockRepository
                .Setup(r => r.EditAsync(It.Is<Employee>(e => e.Login == newEmployee.Login)))
                .ReturnsAsync(employee);
            return this;
        }
        public EmployeeRepositoryBuilder WithLockAsync(string login, Employee employee)
        {
            _mockRepository
                .Setup(r => r.LockAsync(login))
                .ReturnsAsync(employee);
            return this;
        }

        public EmployeeRepositoryBuilder WithUnlockAsync(string login, Employee employee)
        {
            _mockRepository
                .Setup(r => r.UnlockAsync(login))
                .ReturnsAsync(employee);
            return this;
        }

        public EmployeeRepositoryBuilder WithDeleteAsync(string login, Employee employee)
        {
            _mockRepository
                .Setup(r => r.DeleteAsync(login))
                .ReturnsAsync(employee);
            return this;
        }

        public IEmployeeRepository Build()
        {
            return _mockRepository.Object;
        }
    }
}
