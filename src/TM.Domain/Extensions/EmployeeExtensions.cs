using System;
using System.Security.Cryptography;
using System.Text;
using TM.Domain.Entities;

namespace TM.Domain.Extensions
{
    public static class EmployeeExtensions
    {
        public static bool ToCreateValid(this Employee employee)
        {
            if (employee == null) 
                throw new NullReferenceException("Employee не может быть null");

            var errorStr = new StringBuilder();

            if (employee.Id != 0)
                errorStr.Append("Employee.Id не должен быть задан;\n");

            if (string.IsNullOrEmpty(employee.Login))
                errorStr.Append("Employee.Login не может быть null");

            //TODO: Другие проверки.

            if(string.IsNullOrEmpty(errorStr.ToString()))
                return true;

            throw new Exception(errorStr.ToString());
        }

        public static bool ToEditValid(this Employee employee)
        {
            if (employee == null)
                throw new NullReferenceException("Employee не может быть null");

            var errorStr = new StringBuilder();

            if (employee.Id == 0)
                errorStr.Append("Employee.Id должен быть задан");

            if (string.IsNullOrEmpty(employee.Login))
                errorStr.Append("Employee.Login не может быть null");

            //TODO: Другие проверки.

            if (string.IsNullOrEmpty(errorStr.ToString()))
                return true;

            throw new Exception(errorStr.ToString());
        }

        public static bool IsWork(this Employee employee)
        {
            if(employee.Status == EmployeeStatus.Work) 
                return true;

            return false;
        }

        public static string ComputeHashedPassword(string password)
        {
            return Convert.ToHexString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
