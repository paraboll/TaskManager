using System;
using System.Collections.Generic;
using System.Linq;
using TM.Domain.Entities;
using TM.WebServer.Entities;

namespace TM.WebServer.Extensions
{
    public static class BuisnessTaskExtensions
    {
        public static UIBuisnessTaskDTO ConvertTo(this BuisnessTask buisnessTask)
        {
            return new UIBuisnessTaskDTO()
            {
                ExternalId = buisnessTask.ExternalId,
                Name = buisnessTask.Name,
                Description = buisnessTask.Description,

                AuthorLogin = buisnessTask.Author.Login,
                EmployeeLogin = buisnessTask.Employee?.Login,

                Status = buisnessTask.Status.ToString(),
                TaskComments = buisnessTask.TaskComments?
                    .Select(_ => _.ConvertTo())
                    .ToList()
            };
        }

        public static BuisnessTask ConvertTo(this UIBuisnessTaskDTO buisnessTask)
        {
            return new BuisnessTask()
            {
                ExternalId = buisnessTask.ExternalId,
                Name = buisnessTask.Name,
                Description = buisnessTask.Description,

                Author = new Employee() { Login = buisnessTask.AuthorLogin},
                Employee = new Employee() { Login = buisnessTask.EmployeeLogin },

                Status = ConvertStatus(buisnessTask.Status),
                /*TaskComments = buisnessTask.TaskComments.Any() 
                                ? buisnessTask.TaskComments 
                                : new List<TaskComment>() */
                TaskComments = buisnessTask.TaskComments
                    .Select(_ => _.ConvertTo())
                    .ToList()
            };
        }

        public static BuisnessTaskStatus ConvertStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
                return BuisnessTaskStatus.None;

            switch (status.ToLower())
            {
                case "waitingstart":
                    return BuisnessTaskStatus.WaitingStart;
                case "attest":
                    return BuisnessTaskStatus.AtTest;
                case "atwork":
                    return BuisnessTaskStatus.AtWork;
                case "finish":
                    return BuisnessTaskStatus.Finish;
                default:
                    throw new Exception($"Не удалось определить статус ({status}) задачи");
            }
        }
    }
}
