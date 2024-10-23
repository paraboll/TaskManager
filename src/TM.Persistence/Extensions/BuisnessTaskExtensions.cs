using Microsoft.EntityFrameworkCore;
using TM.Domain.Entities;

namespace TM.Persistence.Extensions
{
    internal static class BuisnessTaskExtensions
    {
        public static void BuisnessTaskModeling(this ModelBuilder modelBuilder)
        {
            //TODO: не работает c пакетом InMemory. (https://github.com/dotnet/efcore/issues/3850)
            modelBuilder.Entity<BuisnessTask>()
                .HasIndex(b => b.ExternalId).IsUnique();

            modelBuilder.Entity<TaskComment>()
                .HasIndex(b => b.ExternalId).IsUnique();
        }
    }
}
