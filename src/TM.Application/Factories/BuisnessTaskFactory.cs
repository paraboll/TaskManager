using System;
using System.Collections.Generic;
using System.Linq;
using TM.Domain.Entities;

namespace TM.Application.Factories
{
    public static class BuisnessTaskFactory
    {
        public static IEnumerable<BuisnessTask> CreateBuisnessTask(int count = 100)
        {
            for (int i = 0; i < count; i++)
            {
                //Здесь можно "поиграть" временем задержки.
                //Task.Delay(1).Wait();
                yield return new BuisnessTask()
                {
                    //Id = i,
                    ExternalId = $"ExternalId{i}",
                    Name = $"Name{i}",
                    Description = $"Description{i}",
                    AuthorId = (i % 5) + 1,
                    EmployeeId = (i % 5) + 1,
                    Status = (BuisnessTaskStatus)(i % 5),
                    TaskComments = Enumerable.Range(0, 5).Select(j =>
                    {
                        return new TaskComment()
                        {
                            ExternalId = $"CommentExternalId{j}",
                            Time = DateTime.Now,
                            Text = $"Text{j}",
                            BuisnessTaskId = j + 1,
                            AuthorId = (j % 5) + 1
                        };
                    }).ToList()
                };
            }
        }
    }
}
