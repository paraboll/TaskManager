using System.Collections.Generic;
using TM.Application.Interfaces;
using TM.Domain.Entities;

namespace TM.Application.Tests.Mocks
{
    public static class EmployeeFactory
    {
        public static IEmployeeRepository MockAuthorizationService()
        {
            var mockRepository = new EmployeeRepositoryBuilder()
                .WithGetByLoginAsync("AuthorizationGood",
                    new Employee() { Login = "AuthorizationGood", Password = "AuthorizationGood" })
                .WithGetByLoginAsync("AuthorizationBad",
                    new Employee() { Login = "AuthorizationBad", Password = "AuthorizationBad:(" })
                .Build();

            return mockRepository;
        }

        public static IEmployeeRepository MockEmployeeService()
        {
            var mockRepository = new EmployeeRepositoryBuilder()
                .WithGetAllAsync(new List<Employee>
                {
                    new Employee { Login = "login1" },
                    new Employee { Login = "login2" }
                })
                .WithGetByLoginAsync("login1", new Employee { Login = "login1", Password = "login1" })
                .WithGetByLoginAsync("login5", null)
                .WithSearchByLoginAsync("login1", new List<Employee>
                {
                    new Employee { Login = "login1", Password = "login1" },
                    new Employee { Login = "login10", Password = "login10" }
                })
                .WithSearchByLoginAsync("login555", new List<Employee>())
                .WithAddAsync(
                    new Employee() { Login = "Login123", FirstName = "FirstName123", LastName = "LastName123", MiddleName = "MiddleName123", Password = "Password123", Status = EmployeeStatus.Lock }, 
                    new Employee() { Id = 123, Login = "Login123", FirstName = "FirstName123", LastName = "LastName123", MiddleName = "MiddleName123", Password = "Password123", Status = EmployeeStatus.Lock }
                    )
                .WithEditAsync(
                    new Employee() {Id = 123, Login = "Login123", FirstName = "FirstName123", LastName = "LastName123", MiddleName = "MiddleName123", Password = "Password123", },
                    new Employee() { Id = 123, Login = "Login123", FirstName = "FirstName123", LastName = "LastName123", MiddleName = "MiddleName123", Password = "Password123", }
                    )
                .WithGetByLoginAsync("login10", new Employee { Login = "login10", Status = EmployeeStatus.Work })
                .WithGetByLoginAsync("login11", new Employee { Login = "login11", Status = EmployeeStatus.Lock })
                .WithLockAsync("login10", new Employee { Login = "login10", Status = EmployeeStatus.Lock })

                .WithGetByLoginAsync("login20", new Employee { Login = "login20", Status = EmployeeStatus.Lock })
                .WithGetByLoginAsync("login21", new Employee { Login = "login21", Status = EmployeeStatus.Work })
                .WithUnlockAsync("login20", new Employee { Login = "login20", Status = EmployeeStatus.Work })

                .WithGetByLoginAsync("login30", new Employee { Login = "login30", Status = EmployeeStatus.Lock })
                .WithGetByLoginAsync("login31", new Employee { Login = "login31", Status = EmployeeStatus.Work })
                .WithDeleteAsync("login30", new Employee { Login = "login30", Status = EmployeeStatus.Work })

                .WithGetByLoginAsync("login40", new Employee { Login = "login40", Status = EmployeeStatus.Delete })
                .WithGetByLoginAsync("login41", new Employee { Login = "login41", Status = EmployeeStatus.Work })
                .WithLockAsync("login40", new Employee { Login = "login40", Status = EmployeeStatus.Lock })

                .Build();

            return mockRepository;
        }
    }
}
