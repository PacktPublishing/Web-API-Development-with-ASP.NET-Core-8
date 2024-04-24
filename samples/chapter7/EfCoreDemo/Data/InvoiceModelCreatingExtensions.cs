using EfCoreDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDemo.Data;

public static class InvoiceModelCreatingExtensions
{
    public static void ConfigureInvoice(this ModelBuilder builder)
    {
        builder.Entity<Invoice>(b =>
        {
            b.ToTable("Invoices");
            b.HasKey(i => i.Id);
            b.Property(p => p.Id).HasColumnName("Id");
            b.Property(p => p.InvoiceNumber).HasColumnName("InvoiceNumber").HasColumnType("varchar(32)").IsRequired();
            b.Property(p => p.ContactName).HasColumnName("ContactName").HasMaxLength(32).IsRequired();
            b.Property(p => p.Description).HasColumnName("Description").HasMaxLength(256);
            // b.Property(p => p.Amount).HasColumnName("Amount").HasColumnType("decimal(18,2)").IsRequired();
            b.Property(p => p.Amount).HasColumnName("Amount").HasPrecision(18, 2);
            b.Property(p => p.InvoiceDate).HasColumnName("InvoiceDate").HasColumnType("datetimeoffset").IsRequired();
            b.Property(p => p.DueDate).HasColumnName("DueDate").HasColumnType("datetimeoffset").IsRequired();
            b.Property(p => p.Status).HasColumnName("Status").HasMaxLength(16).HasConversion(
                v => v.ToString(),
                v => (InvoiceStatus)Enum.Parse(typeof(InvoiceStatus), v));
        });
    }
}