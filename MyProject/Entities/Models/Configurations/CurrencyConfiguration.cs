﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(c => c.Id).HasName("pk_constraint_Currency");
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.Name).IsRequired().HasColumnName("Name");
        }
    }
}
