using System;
using System.Collections;
using System.Collections.Generic;
using TM.Domain.Entities;
using Xunit;

namespace TM.Domain.Tests
{
    public class TaskCommentComparerTests
    {
        [Theory]
        [ClassData(typeof(TestTaskCommentData))]
        public void Comparer(TaskComment taskComment1, TaskComment taskComment2, bool expected)
        {
            bool areEqual = taskComment1.Equals(taskComment2);

            Assert.Equal(expected, areEqual);
        }
    }

    internal class TestTaskCommentData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new TaskComment() { ExternalId = "ExternalId1", AuthorId = 1, BuisnessTaskId = 1, Text = "Text1", Time = new DateTime(2000, 12, 15)},
                new TaskComment() { ExternalId = "ExternalId1", AuthorId = 1, BuisnessTaskId = 1, Text = "Text1", Time = new DateTime(2000, 12, 15)},
                true
            };
            yield return new object[]
            {
                 new TaskComment() { ExternalId = "ExternalId1", AuthorId = 2, BuisnessTaskId = 1, Text = "Text1", Time = new DateTime(2000, 12, 15)},
                new TaskComment() { ExternalId = "ExternalId1", AuthorId = 1, BuisnessTaskId = 1, Text = "Text1", Time = new DateTime(2000, 12, 15)},
                false
            };
            yield return new object[]
            {
                 new TaskComment() { ExternalId = "ExternalId2", AuthorId = 2, BuisnessTaskId = 1, Text = "Text1", Time = new DateTime(2000, 12, 15)},
                new TaskComment() { ExternalId = "ExternalId3", AuthorId = 1, BuisnessTaskId = 1, Text = "Text2", Time = new DateTime(2000, 12, 15)},
                false
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
