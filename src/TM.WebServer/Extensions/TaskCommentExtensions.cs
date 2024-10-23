using TM.Domain.Entities;
using TM.WebServer.Entities;

namespace TM.WebServer.Extensions
{
    public static class TaskCommentExtensions
    {
        public static UITaskCommentDTO ConvertTo(this TaskComment taskComment)
        {
            return new UITaskCommentDTO()
            {
                ExternalId = taskComment.ExternalId,
                 
                Text = taskComment.Text,
                Time = taskComment.Time,

                AuthorLogin = taskComment.Author.Login,
                BuisnessTaskexternalId = taskComment.BuisnessTask.ExternalId,
            };
        }

        public static TaskComment ConvertTo(this UITaskCommentDTO taskComment)
        {
            return new TaskComment()
            {
                ExternalId = taskComment.ExternalId,

                Text = taskComment.Text,
                Time = taskComment.Time,

                Author = new Employee()
                {
                    Login = taskComment.AuthorLogin
                },

                BuisnessTask = new BuisnessTask()
                {
                    ExternalId = taskComment.BuisnessTaskexternalId
                }
            };
        }
    }
}
