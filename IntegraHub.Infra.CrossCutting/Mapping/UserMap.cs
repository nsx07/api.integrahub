using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using IntegraHub.Domain.Entities;

namespace IntegraHub.Infra.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Id)
                .HasColumnName(nameof(User.Id).ToLower())
                .ValueGeneratedOnAdd();

            builder.Property(prop => prop.CompanyId)
                .HasColumnName(nameof(User.CompanyId).ToLower())
                .IsRequired();

            builder.Property(prop => prop.Name)
                .HasColumnName(nameof(User.Name).ToLower())
                .IsRequired();

            builder.Property(prop => prop.Email)
               .HasColumnName(nameof(User.Email).ToLower())
               .IsRequired();

            builder.Property(prop => prop.Password)
                .HasColumnName(nameof(User.Password).ToLower())
                .IsRequired();

            builder.Property(prop => prop.DateBirth)
                .HasColumnName(nameof(User.DateBirth).ToLower())
                .IsRequired();

            builder.Property(prop => prop.IsActive)
                .HasColumnName(nameof(User.IsActive).ToLower())
                .IsRequired();

            builder.Property(prop => prop.Surname)
                .HasColumnName(nameof(User.Surname).ToLower())
                .IsRequired();

            builder.Property(prop => prop.Phone)
                .HasColumnName(nameof(User.Phone).ToLower())
                .IsRequired();

            builder.Property(prop => prop.Color)
                .HasColumnName(nameof(User.Color).ToLower());

            builder.Property(prop => prop.UserTypeId)
                .HasColumnName(nameof(User.UserTypeId).ToLower())
                .IsRequired();
           
            builder.HasOne(prop => prop.UserType)
                .WithMany()
                .HasForeignKey(prop => prop.Id);

            builder.HasIndex(prop => new { prop.Email, prop.CompanyId})
                .IsUnique();

            builder.HasOne(prop => prop.Company)
                .WithMany(prop => prop.Users)
                .HasForeignKey(prop => prop.CompanyId);

        }
    }
}
