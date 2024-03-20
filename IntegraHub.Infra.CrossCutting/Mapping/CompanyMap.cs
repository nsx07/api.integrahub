using IntegraHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Infra.Data.Mapping
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");

            builder.HasKey(x => x.Id);

            builder.Property(prop => prop.Id)
                .HasColumnName(nameof(Company.Id).ToLower())
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(prop => prop.FullName)
                .HasColumnName(nameof(Company.FullName).ToLower())
                .IsRequired();

            builder.Property(prop => prop.DomainName)
                .HasColumnName(nameof(Company.DomainName).ToLower())
                .IsRequired();  

            builder.Property(prop => prop.Email)
                .HasColumnName(nameof(Company.Email).ToLower())
                .IsRequired();

            builder.Property(prop => prop.State)
                .HasColumnName(nameof(Company.State).ToLower())
                .IsRequired();

            builder.Property(prop => prop.City)
                .HasColumnName(nameof(Company.City).ToLower())
                .IsRequired();

            builder.Property(prop => prop.Address)
                .HasColumnName(nameof(Company.Address).ToLower())
                .IsRequired();

            builder.Property(prop => prop.Number)
                .HasColumnName(nameof(Company.Number).ToLower())
                .IsRequired();

            builder.Property(prop => prop.ZipCode)
                .HasColumnName(nameof(Company.ZipCode).ToLower())
                .IsRequired();

            builder.Property(prop => prop.Phone)
                .HasColumnName(nameof(Company.Phone).ToLower())
                .IsRequired();

            builder.Property(prop => prop.LogoUrl)
                .HasColumnName(nameof(Company.LogoUrl).ToLower());

            builder.Property(prop => prop.IsActive)
                .HasColumnName(nameof(Company.IsActive).ToLower());

            builder.Property(prop => prop.CreatedAt)
                .HasColumnName(nameof(Company.CreatedAt).ToLower());

            builder.Property(prop => prop.UpdatedAt)
                .HasColumnName(nameof(Company.UpdatedAt).ToLower());

            builder.HasMany(prop => prop.Users)
                .WithOne(prop => prop.Company)
                .HasForeignKey(prop => prop.CompanyId);

            builder.HasMany(prop => prop.CompanyParameters)
                .WithOne(prop => prop.Company)
                .HasForeignKey(prop => prop.CompanyId);

        }
    }
}
