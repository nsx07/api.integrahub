using IntegraHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Infra.Data.Mapping
{
    public class CompanyParameterMap : IEntityTypeConfiguration<CompanyParameter>
    {
        public void Configure(EntityTypeBuilder<CompanyParameter> builder)
        {
            builder.ToTable("CompanyParameter");

            builder.HasKey(x => x.Id);

            builder.Property(prop => prop.Id)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn()
                    .HasColumnName(nameof(CompanyParameter.Id).ToLower())
                    .IsRequired();

            builder.Property(prop => prop.CompanyId)
                    .HasColumnName(nameof(CompanyParameter.CompanyId).ToLower())
                    .IsRequired();

            builder.Property(prop => prop.Name)
                    .HasColumnName(nameof(CompanyParameter.Name).ToLower())
                    .IsRequired();

            builder.Property(prop => prop.Value)
                    .HasColumnName(nameof(CompanyParameter.Value).ToLower())
                    .IsRequired();

            builder.HasOne(prop => prop.Company)
                    .WithMany(prop => prop.CompanyParameters)
                    .HasForeignKey(prop => prop.CompanyId);

        }
    }
}
