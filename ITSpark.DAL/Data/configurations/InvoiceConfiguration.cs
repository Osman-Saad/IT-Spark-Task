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
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id)
                .ValueGeneratedOnAdd();

            builder.Property(I => I.Type)
                .HasConversion(
                IT => IT.ToString(),
                IT => (InvoiceType)Enum.Parse(typeof(InvoiceType), IT)
                );

            builder.HasMany(I => I.Items)
                .WithOne(II => II.Invoice)
                .HasForeignKey(II => II.InvoiceId);
        }
    }
}
