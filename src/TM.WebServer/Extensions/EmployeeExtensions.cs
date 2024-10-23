using System;
using TM.Domain.Entities;
using TM.WebServer.Entities;

namespace TM.WebServer.Extensions
{
    public static class EmployeeExtensions
    {
        public static UIEmployeeDTO ConvertTo(this Employee employee)
        {
            return new UIEmployeeDTO()
            {
                Login = employee.Login,

                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,

                Status = employee.Status.ToString(),
                Password = employee.HashPassword
            };
        }

        public static Employee ConvertTo(this UIEmployeeDTO employee)
        {
            return new Employee()
            {
                Login = employee.Login,

                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,

                Status = ConvertStatus(employee.Status),
                Password = employee.Password
            };
        }

        public static EmployeeStatus ConvertStatus(string status)
        {
            switch (status.ToLower())
            {
                case "work":
                    return EmployeeStatus.Work;
                case "lock":
                    return EmployeeStatus.Lock;
                case "delete":
                    return EmployeeStatus.Delete;
                default:
                    throw new Exception($"Не удалось определить статус ({status}) сотрудника");
            }
        }
    }
}
