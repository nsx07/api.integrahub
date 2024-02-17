using IntegraHub.Domain.Entities;
using IntegraHub.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace IntegraHub.Infra.Data.Context
{
    public class PostgresContext(DbContextOptions<PostgresContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(new UserMap().Configure);
        }
    }
}
