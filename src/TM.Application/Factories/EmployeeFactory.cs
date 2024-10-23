using System.Collections.Generic;
using TM.Domain.Entities;

namespace TM.Application.Factories
{
    public static class EmployeeFactory
    {
        public static IEnumerable<Employee> CreateEmployees(int count = 10)
        {
            for (int i = 0; i < count; i++)
            {
                //Здесь можно "поиграть" временем задержки.
                //Task.Delay(1).Wait();
                yield return new Employee()
                {
                    //Id = i,
                    Login = $"Login{i}",
                    FirstName = $"FirstName{i}",
                    MiddleName = $"MiddleName{i}",
                    LastName = $"LastName{i}",
                    Password =  $"Password{i}",
                    Status = (EmployeeStatus)(i % 3)
                };
            }
        }
    }
}
