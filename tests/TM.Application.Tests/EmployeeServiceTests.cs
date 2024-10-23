using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Application.Interfaces;
using TM.Application.Services;
using TM.Application.Tests.Mocks;
using TM.Domain.Entities;
using Xunit;

namespace TM.Application.Tests
{
    public class EmployeeServiceTests
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            var mockLogger = LoggerFactory.Mock<IEmployeeService>();

            _employeeService = new EmployeeService(mockLogger, EmployeeFactory.MockEmployeeService());
        }

        [Fact]
        public async Task Test_GetAllAsync()
        {
            var actual = await _employeeService.GetAllAsync();

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
        }

        [Theory]
        [ClassData(typeof(TestEmployeeData_GetByLoginAsync))]
        public async Task Test_GetByLoginAsync(string login, Employee expected)
        {
            var employee = await _employeeService.GetByLoginAsync(login);

            Assert.Equal(expected, employee);
        }

        [Theory]
        [ClassData(typeof(TestEmployeeData_SearchByLoginAsync))]
        public async Task Test_SearchByLoginAsync(string login, List<Employee> expected)
        {
            var employees = await _employeeService.SearchByLoginAsync(login);

            Assert.NotNull(employees);

            Assert.Equal(expected, employees);
        }

        [Fact]
        public async Task Test_CreateAsync()
        {
            var employee = new Employee()
            {
                Login = "Login123",
                FirstName = "FirstName123",
                LastName = "LastName123",
                MiddleName = "MiddleName123",
                Password = "Password123",
            };

            var newEmployee = await _employeeService.CreateAsync(employee);

            employee.Id = 123;
            employee.Status = EmployeeStatus.Lock;

            Assert.Equal(employee, newEmployee);
        }

        [Fact]
        public async Task Test_EditAsync()
        {
            var employee = new Employee()
            {
                Id = 123,
                Login = "Login123",
                FirstName = "FirstName123",
                LastName = "LastName123",
                MiddleName = "MiddleName123"
            };

            var editEmployee = await _employeeService.EditAsync(employee);

            employee.Password = "Password123";

            Assert.Equal(employee, editEmployee);
        }

        [Theory]
        [InlineData("login10", EmployeeStatus.Lock, "")]
        [InlineData("login11", null, "Не удалось заблокировать сотрудника login11:Lock")]
        public async Task Test_LockAsync(string login, EmployeeStatus expected, string errorStr)
        {
            try
            {
                var employee = await _employeeService.LockAsync(login);

                Assert.Equal(expected, employee.Status);
            }
            catch (Exception exc)
            {
                Assert.Equal(errorStr, exc.Message);
            }
        }

        [Theory]
        [InlineData("login20", EmployeeStatus.Work, "")]
        [InlineData("login21", null, "Не удалось разблокировать сотрудника login21:Work")]
        public async Task Test_UnlockAsync(string login, EmployeeStatus expected, string errorStr)
        {
            try
            {
                var employee = await _employeeService.UnlockAsync(login);

                Assert.Equal(expected, employee.Status);
            }
            catch (Exception exc)
            {
                Assert.Equal(errorStr, exc.Message);
            }
        }

        [Theory]
        [InlineData("login30", EmployeeStatus.Work, "")]
        [InlineData("login31", null, "Не удалось удалить сотрудника login31:Work")]
        public async Task Test_DeleteAsync(string login, EmployeeStatus expected, string errorStr)
        {
            try
            {
                var employee = await _employeeService.DeleteAsync(login);

                Assert.Equal(expected, employee.Status);
            }
            catch (Exception exc)
            {
                Assert.Equal(errorStr, exc.Message);
            }
        }

        [Theory]
        [InlineData("login40", EmployeeStatus.Lock, "")]
        [InlineData("login41", null, "Не удалось восстановить сотрудника login41:Work")]
        public async Task Test_ReinstateAsync(string login, EmployeeStatus expected, string errorStr)
        {
            try
            {
                var employee = await _employeeService.ReinstateAsync(login);

                Assert.Equal(expected, employee.Status);
            }
            catch (Exception exc)
            {
                Assert.Equal(errorStr, exc.Message);
            }
        }
    }
}
