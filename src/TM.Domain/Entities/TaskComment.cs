using System;

namespace TM.Domain.Entities
{
    public class TaskComment
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }

        public string Text { get; set; }
        public DateTime Time { get; set; }

        public int BuisnessTaskId { get; set; }
        public int AuthorId { get; set; }

        public BuisnessTask BuisnessTask { get; set; }
        public Employee Author { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is TaskComment other)
            {
                return Id == other.Id &&
                       ExternalId == other.ExternalId &&
                       Text == other.Text &&
                       Time == other.Time &&
                       AuthorId == other.AuthorId &&
                       BuisnessTaskId == other.BuisnessTaskId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ExternalId, Text, Time, BuisnessTaskId, AuthorId);
        }
    }
}
