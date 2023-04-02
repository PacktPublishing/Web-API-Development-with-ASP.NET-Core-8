using EfCoreRelationshipsDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreRelationshipsDemo.Data;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.FirstName).IsRequired();
        builder.Property(c => c.LastName).IsRequired();
        builder.Property(c => c.Email).IsRequired();
        builder.Property(c => c.Phone).IsRequired();
        builder.HasOne(c => c.Address)
            .WithOne(a => a.Contact)
            .HasForeignKey<Address>(a => a.ContactId);

        // Use owned entity type
        // builder.OwnsOne(c => c.Address, a =>
        // {
        //     a.WithOwner(x => x.Contact);
        //     a.Property(a => a.Street).HasColumnName("Street").HasMaxLength(64).IsRequired();
        //     a.Property(a => a.City).HasColumnName("City").HasMaxLength(32).IsRequired();
        //     a.Property(a => a.State).HasColumnName("State").HasMaxLength(32).IsRequired();
        //     a.Property(a => a.ZipCode).HasColumnName("ZipCode").HasMaxLength(16).IsRequired();
        // });
    }
}
