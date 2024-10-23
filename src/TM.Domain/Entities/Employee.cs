using System;
using TM.Domain.Extensions;

namespace TM.Domain.Entities
{
    public sealed class Employee
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string HashPassword { get; set; }
        public EmployeeStatus Status { get; set; }


        private string _password;
        public string Password
        {
            set
            {
                _password = value;
                HashPassword = EmployeeExtensions.ComputeHashedPassword(_password);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Employee other)
            {
                return Id == other.Id &&
                       Login == other.Login &&
                       FirstName == other.FirstName &&
                       MiddleName == other.MiddleName &&
                       LastName == other.LastName &&
                       HashPassword == other.HashPassword &&
                       Status == other.Status;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Login, FirstName, MiddleName, LastName, HashPassword, Status);
        }
    }

    public enum EmployeeStatus
    {
        Work,
        Lock,
        Delete
    }
}
