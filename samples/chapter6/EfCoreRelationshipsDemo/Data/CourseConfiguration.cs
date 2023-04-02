//using EfCoreRelationshipsDemo.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace EfCoreRelationshipsDemo.Data;

//public class CourseConfiguration : IEntityTypeConfiguration<Course>
//{
//    public void Configure(EntityTypeBuilder<Course> builder)
//    {
//        builder.ToTable("Courses");
//        builder.HasKey(c => c.Id);
//        builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(64).IsRequired();
//        builder.Property(p => p.Description).HasColumnName("Description").HasMaxLength(512).IsRequired();
//    }
//}
