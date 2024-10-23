using Microsoft.EntityFrameworkCore;
using TM.Domain.Entities;

namespace TM.Persistence.Extensions
{
    internal static class EmployeeExtensions
    {
        public static void EmployeeModeling(this ModelBuilder modelBuilder)
        {
            //TODO: не работает c пакетом InMemory. (https://github.com/dotnet/efcore/issues/3850)
            modelBuilder.Entity<Employee>()
                .HasIndex(b => b.Login).IsUnique();
        }
    }
}
