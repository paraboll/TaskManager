using System;
using System.Collections.Generic;
using System.Linq;

namespace TM.Domain.Entities
{
    public sealed class BuisnessTask
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public int? EmployeeId { get; set; }
        public BuisnessTaskStatus Status { get; set; }

        public Employee Author { get; set; }
        public Employee? Employee { get; set; }

        public List<TaskComment> TaskComments { get; set; }

        //TODO: Отмечалка времени

        public override bool Equals(object obj)
        {
            if (obj is BuisnessTask other)
            {
                return Id == other.Id &&
                       ExternalId == other.ExternalId &&
                       Name == other.Name &&
                       Description == other.Description &&
                       AuthorId == other.AuthorId &&
                       EmployeeId == other.EmployeeId &&
                       Status == other.Status &&
                       TaskComments.SequenceEqual(other.TaskComments);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ExternalId, Name, Description, AuthorId,
                EmployeeId, Status, TaskComments.Select(_ => _.GetHashCode()));
        }
    }

    public enum BuisnessTaskStatus
    {
        None,
        WaitingStart,
        AtWork,
        AtTest,
        Finish
    }
}
