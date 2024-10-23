using System;
using TM.Domain.Entities;

namespace TM.Domain.Extensions
{
    public static class BuisnessTaskExtensions
    {
        public static bool ToCreateValid(this BuisnessTask buisnessTask)
        {
            if (buisnessTask == null)
                throw new NullReferenceException("BuisnessTask не может быть null");

            if (buisnessTask.Id != 0)
                throw new Exception("BuisnessTask.Id не должен быть задан");

            if (string.IsNullOrEmpty(buisnessTask.Name))
                throw new NullReferenceException("BuisnessTask.Name не может быть null");

            if (buisnessTask.Author == null || string.IsNullOrEmpty(buisnessTask.Author.Login))
                throw new Exception("BuisnessTask.Author должен быть задан");

            //TODO: Другие проверки.

            return true;
        }

        public static bool ToEditValid(this BuisnessTask buisnessTask)
        {
            if (buisnessTask == null)
                throw new NullReferenceException("BuisnessTask не может быть null");

            if (buisnessTask.Id == 0)
                throw new Exception("BuisnessTask.Id должен быть задан");

            if (string.IsNullOrEmpty(buisnessTask.Name))
                throw new NullReferenceException("BuisnessTask.Name не может быть null");

            if (buisnessTask.Author == null || string.IsNullOrEmpty(buisnessTask.Author.Login))
                throw new Exception("BuisnessTask.Author должен быть задан");

            //TODO: Другие проверки.

            return true;
        }
    }
}
