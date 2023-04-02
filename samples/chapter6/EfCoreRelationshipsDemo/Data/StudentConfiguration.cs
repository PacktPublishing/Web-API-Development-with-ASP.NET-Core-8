//using EfCoreRelationshipsDemo.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace EfCoreRelationshipsDemo.Data;

//public class StudentConfiguration : IEntityTypeConfiguration<Student>
//{
//    public void Configure(EntityTypeBuilder<Student> builder)
//    {
//        builder.ToTable("Students");
//        builder.HasKey(s => s.Id);
//        builder.Property(p => p.FirstName).HasColumnName("FirstName").HasMaxLength(32).IsRequired();
//        builder.Property(p => p.LastName).HasColumnName("LastName").HasMaxLength(32).IsRequired();
//        builder.Property(p => p.Email).HasColumnName("Email").HasMaxLength(64).IsRequired();
//    }
//}
