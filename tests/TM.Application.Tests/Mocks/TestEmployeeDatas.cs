using System.Collections;
using System.Collections.Generic;
using TM.Domain.Entities;

namespace TM.Application.Tests.Mocks
{
    public class TestEmployeeData_GetByLoginAsync : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "login1", new Employee() { Login = "login1", Password = "login1" } };
            yield return new object[] { "login5", null };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TestEmployeeData_SearchByLoginAsync : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] 
            { 
                "login1", 
                new List<Employee>()
                {
                    new Employee() { Login = "login1", Password = "login1" },
                    new Employee() { Login = "login10", Password = "login10" }
                }
            };
            yield return new object[] { "login555", new List<Employee>() };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
