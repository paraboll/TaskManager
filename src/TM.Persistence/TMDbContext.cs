using TM.Domain.Entities;
using TM.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TM.Persistence
{
    public class TMDbContext: DbContext
    {
        protected const string schemaName = "TM_SCHEMA";

        public DbSet<Employee> Employees { get; set; }
        public DbSet<BuisnessTask> BuisnessTasks { get; set; }


        public TMDbContext() { }

        [ActivatorUtilitiesConstructor]
        public TMDbContext(DbContextOptions<TMDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schemaName);

            modelBuilder.EmployeeModeling();
            modelBuilder.BuisnessTaskModeling();
        }
    }
}
