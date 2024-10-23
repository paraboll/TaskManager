using System;
using System.Text;
using TM.Domain.Entities;

namespace TM.Domain.Extensions
{
    public static class TaskCommantExtensions
    {
        public static bool ToCreateValid(this TaskComment taskComment)
        {
            if (taskComment == null)
                throw new NullReferenceException("TaskComment не может быть null");

            if (taskComment.Author == null)
                throw new Exception("TaskComment.Author должен быть задан.");

            if (taskComment.BuisnessTask == null)
                throw new Exception("TaskComment.BuisnessTask должен быть задан.");

            var errorStr = new StringBuilder();

            if (!string.IsNullOrEmpty(taskComment.ExternalId))
                errorStr.Append("TaskComment.ExternalId не должен быть задан;\n");

            if (string.IsNullOrEmpty(taskComment.Text))
                errorStr.Append("TaskComment.Text не задан;\n");

            if (string.IsNullOrEmpty(taskComment.Author.Login))
                errorStr.Append("TaskComment.Author Login не задан;\n");

            if (string.IsNullOrEmpty(taskComment.BuisnessTask.ExternalId))
                errorStr.Append("TaskComment.BuisnessTask ExternalId не задан;\n");

            //TODO: Другие проверки.

            if (string.IsNullOrEmpty(errorStr.ToString()))
                return true;

            throw new Exception(errorStr.ToString());
        }


        //TODO: отредактировать.
        public static bool ToEditValid(this TaskComment taskComment)
        {
            if (taskComment == null)
                throw new NullReferenceException("TaskComment не может быть null");

            if (taskComment.Author == null)
                throw new Exception("TaskComment.Author должен быть задан.");

            if (taskComment.BuisnessTask == null)
                throw new Exception("TaskComment.BuisnessTask должен быть задан.");

            var errorStr = new StringBuilder();

            if (string.IsNullOrEmpty(taskComment.ExternalId))
                errorStr.Append("TaskComment.ExternalId должен быть задан;\n");

            if (string.IsNullOrEmpty(taskComment.Text))
                errorStr.Append("TaskComment.Text не задан;\n");

            if (string.IsNullOrEmpty(taskComment.Author.Login))
                errorStr.Append("TaskComment.Author Login не задан;\n");

            if (string.IsNullOrEmpty(taskComment.BuisnessTask.ExternalId))
                errorStr.Append("TaskComment.BuisnessTask ExternalId не задан;\n");

            //TODO: Другие проверки.

            if (string.IsNullOrEmpty(errorStr.ToString()))
                return true;

            throw new Exception(errorStr.ToString());
        }

        public static bool ToRemoveValid(this TaskComment taskComment)
        {
            if (taskComment == null)
                throw new NullReferenceException("TaskComment не может быть null");

            if (taskComment.BuisnessTask == null)
                throw new Exception("TaskComment.BuisnessTask должен быть задан.");

            var errorStr = new StringBuilder();

            if (string.IsNullOrEmpty(taskComment.ExternalId))
                errorStr.Append("TaskComment.ExternalId должен быть задан;\n");

            if (string.IsNullOrEmpty(taskComment.BuisnessTask.ExternalId))
                errorStr.Append("TaskComment.BuisnessTask ExternalId не задан;\n");

            //TODO: Другие проверки.

            if (string.IsNullOrEmpty(errorStr.ToString()))
                return true;

            throw new Exception(errorStr.ToString());
        }
    }
}
