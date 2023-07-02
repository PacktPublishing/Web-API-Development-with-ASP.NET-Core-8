using InvoiceApp.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceApp.WebApi.Data;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");
        builder.HasKey(i => i.Id);
        builder.Property(p => p.Id).HasColumnName(nameof(Invoice.Id));
        builder.Property(p => p.InvoiceNumber).HasColumnName(nameof(Invoice.InvoiceNumber)).HasColumnType("varchar(32)").IsRequired();
        builder.HasIndex(p => p.InvoiceNumber).IsUnique();
        builder.Property(p => p.ContactId).HasColumnName(nameof(Invoice.ContactId)).IsRequired();
        builder.Property(p => p.Description).HasColumnName(nameof(Invoice.Description)).HasMaxLength(256);
        builder.Property(p => p.Amount).HasColumnName(nameof(Invoice.Amount)).HasPrecision(18, 2);
        builder.Property(p => p.InvoiceDate).HasColumnName(nameof(Invoice.InvoiceDate)).HasColumnType("datetimeoffset").IsRequired();
        builder.Property(p => p.DueDate).HasColumnName(nameof(Invoice.DueDate)).HasColumnType("datetimeoffset").IsRequired();
        builder.Property(p => p.Status).HasColumnName(nameof(Invoice.Status)).HasMaxLength(16).HasConversion(
            v => v.ToString(),
            v => (InvoiceStatus)Enum.Parse(typeof(InvoiceStatus), v));

        //Use the owned type to configure the InvoiceItems collection
        builder.OwnsMany(p => p.InvoiceItems, a =>
        {
            a.WithOwner().HasForeignKey(x => x.InvoiceId);
            a.ToTable("InvoiceItems");
            a.Property(p => p.Id).HasColumnName(nameof(InvoiceItem.Id));
            a.Property(p => p.Name).HasColumnName(nameof(InvoiceItem.Name)).HasMaxLength(64).IsRequired();
            a.Property(p => p.Description).HasColumnName(nameof(InvoiceItem.Description)).HasMaxLength(256);
            a.Property(p => p.UnitPrice).HasColumnName(nameof(InvoiceItem.UnitPrice)).HasPrecision(8, 2);
            a.Property(p => p.Quantity).HasColumnName(nameof(InvoiceItem.Quantity)).HasPrecision(8, 2);
            a.Property(p => p.Amount).HasColumnName(nameof(InvoiceItem.Amount)).HasPrecision(18, 2);
            a.Property(p => p.InvoiceId).HasColumnName(nameof(InvoiceItem.InvoiceId));
        });

        builder.HasOne(p => p.Contact).WithMany().HasForeignKey(p => p.ContactId);
    }
}
