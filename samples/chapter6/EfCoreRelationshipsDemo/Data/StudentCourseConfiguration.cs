//using EfCoreRelationshipsDemo.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace EfCoreRelationshipsDemo.Data;

//public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
//{
//    public void Configure(EntityTypeBuilder<StudentCourse> builder)
//    {
//        builder.ToTable("StudentCourses");
//        builder.HasKey(sc => new { sc.StudentId, sc.CourseId });
//        builder.HasOne(sc => sc.Student)
//            .WithMany(s => s.StudentCourses)
//            .HasForeignKey(sc => sc.StudentId);
//        builder.HasOne(sc => sc.Course)
//            .WithMany(c => c.StudentCourses)
//            .HasForeignKey(sc => sc.CourseId);
//    }
//}
