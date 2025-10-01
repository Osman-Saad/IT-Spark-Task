using ITSpark.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpark.DAL.Data.configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id)
                .ValueGeneratedOnAdd();
            builder.Property(I => I.Name)
                .HasMaxLength(50);
            builder.Property(I => I.UnitPrice)
                .HasColumnType("decimal(18,2)");

                
        }
    }
}
