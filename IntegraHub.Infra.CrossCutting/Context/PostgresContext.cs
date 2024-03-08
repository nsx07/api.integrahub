using IntegraHub.Domain.Entities;
using IntegraHub.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace IntegraHub.Infra.Data.Context
{
    public class PostgresContext(DbContextOptions<PostgresContext> options) : DbContext(options)
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyParameter> CompanyParameter { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<UserType>(new UserTypeMap().Configure);
            modelBuilder.Entity<Company>(new CompanyMap().Configure);
            modelBuilder.Entity<CompanyParameter>(new CompanyParameterMap().Configure);
        }
    }
}
