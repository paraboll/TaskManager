using System.Collections;
using System.Collections.Generic;
using TM.Domain.Entities;
using Xunit;

namespace TM.Domain.Tests
{
    public class EmployeeComparerTests
    {
        [Theory]
        [ClassData(typeof(TestEmployeeData))]
        public void Comparer(Employee employee1, Employee employee2, bool expected)
        {
            var areEqual = employee1.Equals(employee2);


            Assert.Equal(expected, areEqual);
        }
    }

    internal class TestEmployeeData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Employee() { Login = "login1", Password = "Password1" },
                new Employee() { Login = "login1", Password = "Password1" },
                true
            };
            yield return new object[]
            {
                new Employee() { Login = "login1", Password = "Password1" },
                new Employee() { Login = "login2", Password = "Password2" },
                false
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
