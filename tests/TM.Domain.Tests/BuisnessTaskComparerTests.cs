using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TM.Domain.Entities;
using Xunit;

namespace TM.Domain.Tests
{
    public class BuisnessTaskComparerTests
    {
        [Theory]
        [ClassData(typeof(TestBuisnessTaskData))]
        public void Comparer(BuisnessTask buisnessTask1, BuisnessTask buisnessTask2, bool expected)
        {
            bool areEqual = buisnessTask1.Equals(buisnessTask2);

            Assert.Equal(expected, areEqual);
        }
    }

    internal class TestBuisnessTaskData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new BuisnessTask() { ExternalId = "ExternalId1", Name = "Name1", Description = "Description1", 
                    AuthorId = 1, EmployeeId = 12, Status = BuisnessTaskStatus.AtWork, TaskComments = new List<TaskComment>()},
                new BuisnessTask() { ExternalId = "ExternalId1", Name = "Name1", Description = "Description1",
                    AuthorId = 1, EmployeeId = 12, Status = BuisnessTaskStatus.AtWork, TaskComments = new List<TaskComment>()},
                true
            };
            yield return new object[]
            {
                new BuisnessTask() { ExternalId = "ExternalId1", Name = "Name1", Description = "Description1",
                    AuthorId = 1, EmployeeId = 12, Status = BuisnessTaskStatus.AtWork, TaskComments = new List<TaskComment>()
                    {
                        new TaskComment() {ExternalId = "ExternalId1", Text = "Text1"},
                        new TaskComment() {ExternalId = "ExternalId2", Text = "Text2"},
                    }},
                new BuisnessTask() { ExternalId = "ExternalId1", Name = "Name1", Description = "Description1",
                    AuthorId = 1, EmployeeId = 12, Status = BuisnessTaskStatus.AtWork, TaskComments = new List<TaskComment>()
                    {
                        new TaskComment() {ExternalId = "ExternalId1", Text = "Text1"},
                        new TaskComment() {ExternalId = "ExternalId2", Text = "Text2"},
                    }},
                true
            };
            yield return new object[]
            {
                new BuisnessTask() { ExternalId = "ExternalId1", Name = "Name1", Description = "Description1",
                    AuthorId = 1, EmployeeId = 12, Status = BuisnessTaskStatus.AtWork, TaskComments = new List<TaskComment>()},
                new BuisnessTask() { ExternalId = "ExternalId2", Name = "Name2", Description = "Description2",
                    AuthorId = 1, EmployeeId = 12, Status = BuisnessTaskStatus.AtWork, TaskComments = new List<TaskComment>()},
                false
            };
            yield return new object[]
            {
                new BuisnessTask() { ExternalId = "ExternalId1", Name = "Name1", Description = "Description1",
                    AuthorId = 1, EmployeeId = 12, Status = BuisnessTaskStatus.AtWork, TaskComments = new List<TaskComment>()
                    {
                        new TaskComment() {ExternalId = "ExternalId1", Text = "Text1"},
                        new TaskComment() {ExternalId = "ExternalId2", Text = "Text2"},
                    }},
                new BuisnessTask() { ExternalId = "ExternalId1", Name = "Name1", Description = "Description1",
                    AuthorId = 1, EmployeeId = 12, Status = BuisnessTaskStatus.AtWork, TaskComments = new List<TaskComment>()
                    {
                        new TaskComment() {ExternalId = "ExternalId1", Text = "Text1"},
                        new TaskComment() {ExternalId = "ExternalId2", Text = "Text2"},
                        new TaskComment() {ExternalId = "ExternalId3", Text = "Text3"},
                    }},
                false
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
