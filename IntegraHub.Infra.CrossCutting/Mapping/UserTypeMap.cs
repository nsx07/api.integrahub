using IntegraHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Infra.Data.Mapping
{
    public class UserTypeMap : IEntityTypeConfiguration<UserType>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable("UserType");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Id)
                .HasColumnName(nameof(UserType.Id).ToLower())
                .ValueGeneratedOnAdd();

            builder.Property(prop => prop.Code)
                .HasColumnName(nameof(UserType.Code).ToLower())
                .IsRequired();

            builder.Property(prop => prop.Description)
                .HasColumnName(nameof(UserType.Description).ToLower())
                .IsRequired();

            builder.HasMany(prop => prop.Users)
                .WithOne(prop => prop.UserType)
                .HasForeignKey(prop => prop.UserTypeId);
        }
    }
}
