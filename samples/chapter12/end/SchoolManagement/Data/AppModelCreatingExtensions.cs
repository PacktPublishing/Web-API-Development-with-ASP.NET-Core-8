using Microsoft.EntityFrameworkCore;

using SchoolManagement.Models;

namespace SchoolManagement.Data;

public static class AppModelCreatingExtensions
{
    public static void ConfigureModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teacher>(b =>
        {
            b.ToTable("Teachers");
            b.HasKey(t => t.Id);
            b.Property(t => t.FirstName).IsRequired().HasMaxLength(32);
            b.Property(t => t.LastName).IsRequired().HasMaxLength(32);
            b.Property(t => t.Email).IsRequired().HasMaxLength(32);
            b.Property(t => t.Phone).HasMaxLength(18);
            b.Property(t => t.Bio).HasMaxLength(500);
            b.Property(t => t.DepartmentId).IsRequired();
            b.Ignore(t => t.Department);
            b.HasOne(t => t.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(t => t.DepartmentId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Student>(b =>
        {
            b.ToTable("Students");
            b.HasKey(s => s.Id);
            b.Property(s => s.FirstName).IsRequired().HasMaxLength(32);
            b.Property(s => s.LastName).IsRequired().HasMaxLength(32);
            b.Property(s => s.Email).IsRequired().HasMaxLength(32);
            b.Property(s => s.Phone).HasMaxLength(18);
            b.Property(s => s.Grade).IsRequired().HasMaxLength(2);
            b.Property(s => s.GroupId).IsRequired();
            b.Ignore(s => s.Group);
            b.HasOne(s => s.Group).WithMany(c => c.Students).HasForeignKey(s => s.GroupId).IsRequired();
        });

        modelBuilder.Entity<Course>(b =>
        {
            b.ToTable("Courses");
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).IsRequired().HasMaxLength(32);
            b.Property(c => c.Description).HasMaxLength(500);
            b.Property(c => c.Credits).IsRequired();
            b.Property(c => c.CourseCode).IsRequired().HasMaxLength(10);
            b.HasIndex(c => c.CourseCode).IsUnique();
            b.Property(c => c.DepartmentId).IsRequired();
            b.Property(c => c.CourseType).HasMaxLength(16).HasConversion(
                v => v.ToString(),
                v => (CourseType)Enum.Parse(typeof(CourseType), v));

            b.HasOne(c => c.Department).WithMany(d => d.Courses).HasForeignKey(c => c.DepartmentId).IsRequired();

            b.HasMany(c => c.Teachers)
                .WithMany(t => t.Courses)
                .UsingEntity<TeacherCourse>(
                    x => x.HasOne<Teacher>().WithMany(y => y.TeacherCourses)
                        .OnDelete(DeleteBehavior.NoAction),
                    x => x.HasOne<Course>().WithMany(y => y.TeacherCourses)
                        .OnDelete(DeleteBehavior.Cascade));
            b.HasMany(c => c.Students)
                .WithMany(s => s.Courses)
                .UsingEntity<StudentCourse>(
                    x => x.HasOne<Student>().WithMany(y => y.StudentCourses)
                        .OnDelete(DeleteBehavior.NoAction),
                    x => x.HasOne<Course>().WithMany(y => y.StudentCourses)
                        .OnDelete(DeleteBehavior.Cascade));

        });

        modelBuilder.Entity<Group>(b =>
        {
            b.ToTable("Groups");
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).IsRequired().HasMaxLength(32);
            b.Property(c => c.GroupCode).IsRequired();
            b.Property(c => c.Description).HasMaxLength(500);
            b.Property(c => c.DepartmentId).IsRequired();
            b.Ignore(c => c.Department);
            //b.Ignore(c => c.Students);
            b.HasOne(c => c.Department).WithMany(d => d.Groups).HasForeignKey(c => c.DepartmentId).IsRequired();
        });

        modelBuilder.Entity<Department>(b =>
        {
            b.ToTable("Departments");
            b.HasKey(d => d.Id);
            b.Property(d => d.Name).IsRequired().HasMaxLength(32);
            b.Property(d => d.Description).HasMaxLength(500);
            //b.Ignore(d => d.Groups);
            //b.Ignore(d => d.Courses);
            //b.Ignore(d => d.Teachers);
        });

        modelBuilder.Entity<LabRoom>(b =>
        {
            b.ToTable("LabRooms");
            b.HasKey(l => l.Id);
            b.Property(l => l.Name).IsRequired().HasMaxLength(32);
            b.Property(l => l.Description).HasMaxLength(500);
            b.Property(l => l.Capacity).IsRequired();
            b.Property(l => l.Equipment).HasMaxLength(128);
            b.Property(l => l.Subject).HasMaxLength(64);
            b.Property(l => l.HasChemicals).IsRequired();
        });

        modelBuilder.Entity<Classroom>(b =>
        {
            b.ToTable("Classrooms");
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).IsRequired().HasMaxLength(32);
            b.Property(c => c.Description).HasMaxLength(500);
            b.Property(c => c.Capacity).IsRequired();
            b.Property(c => c.HasComputers).IsRequired();
            b.Property(c => c.HasProjector).IsRequired();
            b.Property(c => c.HasWhiteboard).IsRequired();
        });

        modelBuilder.Entity<Equipment>(b =>
        {
            b.ToTable("Equipment");
            b.HasKey(e => e.Id);
            b.Property(e => e.Name).IsRequired().HasMaxLength(32);
            b.Property(e => e.Description).HasMaxLength(500);
            b.Property(e => e.Condition).IsRequired().HasMaxLength(32);
            b.Property(e => e.Brand).IsRequired().HasMaxLength(32);
            b.Property(e => e.Quantity).IsRequired();
        });

        modelBuilder.Entity<Furniture>(b =>
        {
            b.ToTable("Furniture");
            b.HasKey(f => f.Id);
            b.Property(f => f.Name).IsRequired().HasMaxLength(32);
            b.Property(f => f.Description).HasMaxLength(500);
            b.Property(f => f.Color).IsRequired().HasMaxLength(32);
            b.Property(f => f.Material).IsRequired().HasMaxLength(32);
            b.Property(e => e.Quantity).IsRequired();
        });
    }

    public static void SeedData(this ModelBuilder modelBuilder)
    {
        var mathDepartmentId = new Guid("00000000-0000-0000-0000-000000000001");
        var scienceDepartmentId = new Guid("00000000-0000-0000-0000-000000000002");
        var computerScienceDepartmentId = new Guid("00000000-0000-0000-0000-000000000003");
        var artsDepartmentId = new Guid("00000000-0000-0000-0000-000000000004");
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = mathDepartmentId, Name = "Mathematics", Description = "Mathematics Department" },
            new Department { Id = scienceDepartmentId, Name = "Science", Description = "Science Department" },
            new Department
            {
                Id = computerScienceDepartmentId,
                Name = "Computer Science",
                Description = "Computer Science Department"
            },
            new Department { Id = artsDepartmentId, Name = "Arts", Description = "Arts Department" }
        );

        var mathGroupId = new Guid("00000000-0000-0000-0000-000000000101");
        var algebraGroupId = new Guid("00000000-0000-0000-0000-000000000102");
        var geometryGroupId = new Guid("00000000-0000-0000-0000-000000000103");
        var scienceGroupId = new Guid("00000000-0000-0000-0000-000000000201");
        var physicsGroupId = new Guid("00000000-0000-0000-0000-000000000202");
        var chemistryGroupId = new Guid("00000000-0000-0000-0000-000000000203");
        var environmentalScienceGroupId = new Guid("00000000-0000-0000-0000-000000000204");
        var computerScienceGroupId = new Guid("00000000-0000-0000-0000-000000000205");
        var computerProgrammingGroupId = new Guid("00000000-0000-0000-0000-000000000206");
        var computerApplicationsGroupId = new Guid("00000000-0000-0000-0000-000000000207");
        var musicGroupId = new Guid("00000000-0000-0000-0000-000000000208");
        var paintingGroupId = new Guid("00000000-0000-0000-0000-000000000209");
        var photographyGroupId = new Guid("00000000-0000-0000-0000-000000000210");
        var danceGroupId = new Guid("00000000-0000-0000-0000-000000000211");
        modelBuilder.Entity<Group>().HasData(
            new Group
            {
                Id = mathGroupId,
                Name = "Mathematics",
                GroupCode = "MATH",
                Description = "Mathematics Group",
                DepartmentId = mathDepartmentId
            },
            new Group
            {
                Id = algebraGroupId,
                Name = "Algebra",
                GroupCode = "ALG",
                Description = "Algebra Group",
                DepartmentId = mathDepartmentId
            },
            new Group
            {
                Id = geometryGroupId,
                Name = "Geometry",
                GroupCode = "GEO",
                Description = "Geometry Group",
                DepartmentId = mathDepartmentId
            },
            new Group
            {
                Id = scienceGroupId,
                Name = "Science",
                GroupCode = "SCI",
                Description = "Science Group",
                DepartmentId = scienceDepartmentId
            },
            new Group
            {
                Id = physicsGroupId,
                Name = "Physics",
                GroupCode = "PHY",
                Description = "Physics Group",
                DepartmentId = scienceDepartmentId
            },
            new Group
            {
                Id = chemistryGroupId,
                Name = "Chemistry",
                GroupCode = "CHEM",
                Description = "Chemistry Group",
                DepartmentId = scienceDepartmentId
            },
            new Group
            {
                Id = environmentalScienceGroupId,
                Name = "Environmental Science",
                GroupCode = "ENV",
                Description = "Environmental Science Group",
                DepartmentId = scienceDepartmentId
            },
            new Group
            {
                Id = computerScienceGroupId,
                Name = "Computer Science",
                GroupCode = "CS",
                Description = "Computer Science Group",
                DepartmentId = computerScienceDepartmentId
            },
            new Group
            {
                Id = computerProgrammingGroupId,
                Name = "Computer Programming",
                GroupCode = "CP",
                Description = "Computer Programming Group",
                DepartmentId = computerScienceDepartmentId
            },
            new Group
            {
                Id = computerApplicationsGroupId,
                Name = "Computer Applications",
                GroupCode = "CA",
                Description = "Computer Applications Group",
                DepartmentId = computerScienceDepartmentId
            },
            new Group
            {
                Id = musicGroupId,
                Name = "Music",
                GroupCode = "MUS",
                Description = "Music Group",
                DepartmentId = artsDepartmentId
            },
            new Group
            {
                Id = paintingGroupId,
                Name = "Painting",
                GroupCode = "PAINT",
                Description = "Painting Group",
                DepartmentId = artsDepartmentId
            },
            new Group
            {
                Id = photographyGroupId,
                Name = "Photography",
                GroupCode = "PHOTO",
                Description = "Photography Group",
                DepartmentId = artsDepartmentId
            },
            new Group
            {
                Id = danceGroupId,
                Name = "Dance",
                GroupCode = "DANCE",
                Description = "Dance Group",
                DepartmentId = artsDepartmentId
            }
        );

        var mathCourseId = new Guid("00000000-0000-0000-0000-000000000301");
        var algebraCourseId = new Guid("00000000-0000-0000-0000-000000000302");
        var geometryCourseId = new Guid("00000000-0000-0000-0000-000000000303");
        var mathFundamentalsCourseId = new Guid("00000000-0000-0000-0000-000000000304");
        var scienceCourseId = new Guid("00000000-0000-0000-0000-000000000305");
        var physicsCourseId = new Guid("00000000-0000-0000-0000-000000000306");
        var chemistryCourseId = new Guid("00000000-0000-0000-0000-000000000307");
        var environmentalScienceCourseId = new Guid("00000000-0000-0000-0000-000000000308");
        var scienceFundamentalsCourseId = new Guid("00000000-0000-0000-0000-000000000309");
        var computerScienceCourseId = new Guid("00000000-0000-0000-0000-000000000310");
        var computerProgrammingCourseId = new Guid("00000000-0000-0000-0000-000000000311");
        var computerApplicationsCourseId = new Guid("00000000-0000-0000-0000-000000000312");
        var computerScienceFundamentalsCourseId = new Guid("00000000-0000-0000-0000-000000000313");
        var dataStructuresCourseId = new Guid("00000000-0000-0000-0000-000000000314");
        var algorithmsCourseId = new Guid("00000000-0000-0000-0000-000000000315");
        var musicCourseId = new Guid("00000000-0000-0000-0000-000000000316");
        var paintingCourseId = new Guid("00000000-0000-0000-0000-000000000317");
        var photographyCourseId = new Guid("00000000-0000-0000-0000-000000000318");
        var danceCourseId = new Guid("00000000-0000-0000-0000-000000000319");
        var artHistoryCourseId = new Guid("00000000-0000-0000-0000-000000000320");
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = mathCourseId,
                Name = "Mathematics",
                CourseCode = "MATH",
                Description = "Mathematics Course",
                Credits = 3,
                CourseType = CourseType.Core,
                DepartmentId = mathDepartmentId
            },
            new Course
            {
                Id = algebraCourseId,
                Name = "Algebra",
                CourseCode = "ALG",
                Description = "Algebra Course",
                Credits = 3,
                CourseType = CourseType.Core,
                DepartmentId = mathDepartmentId
            },
            new Course
            {
                Id = geometryCourseId,
                Name = "Geometry",
                CourseCode = "GEO",
                Description = "Geometry Course",
                Credits = 3,
                CourseType = CourseType.Core,
                DepartmentId = mathDepartmentId
            },
            new Course
            {
                Id = mathFundamentalsCourseId,
                Name = "Math Fundamentals",
                CourseCode = "MATHF",
                Description = "Math Fundamentals Course",
                Credits = 3,
                CourseType = CourseType.Elective,
                DepartmentId = mathDepartmentId
            },
            new Course
            {
                Id = scienceCourseId,
                Name = "Science",
                CourseCode = "SCI",
                Description = "Science Course",
                Credits = 3,
                CourseType = CourseType.Elective,
                DepartmentId = scienceDepartmentId
            },
            new Course
            {
                Id = physicsCourseId,
                Name = "Physics",
                CourseCode = "PHY",
                Description = "Physics Course",
                Credits = 3,
                CourseType = CourseType.Core,
                DepartmentId = scienceDepartmentId
            },
            new Course
            {
                Id = chemistryCourseId,
                Name = "Chemistry",
                CourseCode = "CHEM",
                Description = "Chemistry Course",
                Credits = 3,
                CourseType = CourseType.Lab,
                DepartmentId = scienceDepartmentId
            },
            new Course
            {
                Id = environmentalScienceCourseId,
                Name = "Environmental Science",
                CourseCode = "ENV",
                Description = "Environmental Science Course",
                Credits = 3,
                CourseType = CourseType.Elective,
                DepartmentId = scienceDepartmentId
            },
            new Course
            {
                Id = scienceFundamentalsCourseId,
                Name = "Science Fundamentals",
                CourseCode = "SCIF",
                Description = "Science Fundamentals Course",
                Credits = 3,
                CourseType = CourseType.Elective,
                DepartmentId = scienceDepartmentId
            },
            new Course
            {
                Id = computerScienceCourseId,
                Name = "Computer Science",
                CourseCode = "CS",
                Description = "Computer Science Course",
                Credits = 3,
                CourseType = CourseType.Core,
                DepartmentId = computerScienceDepartmentId
            },
            new Course
            {
                Id = computerProgrammingCourseId,
                Name = "Computer Programming",
                CourseCode = "CP",
                Description = "Computer Programming Course",
                Credits = 3,
                CourseType = CourseType.Core,
                DepartmentId = computerScienceDepartmentId
            },
            new Course
            {
                Id = computerApplicationsCourseId,
                Name = "Computer Applications",
                CourseCode = "CA",
                Description = "Computer Applications Course",
                Credits = 3,
                CourseType = CourseType.Lab,
                DepartmentId = computerScienceDepartmentId
            },
            new Course
            {
                Id = computerScienceFundamentalsCourseId,
                Name = "Computer Science Fundamentals",
                CourseCode = "CSF",
                Description = "Computer Science Fundamentals Course",
                Credits = 3,
                CourseType = CourseType.Elective,
                DepartmentId = computerScienceDepartmentId
            },
            new Course
            {
                Id = dataStructuresCourseId,
                Name = "Data Structures",
                CourseCode = "DS",
                Description = "Data Structures Course",
                Credits = 3,
                CourseType = CourseType.Core,
                DepartmentId = computerScienceDepartmentId
            },
            new Course
            {
                Id = algorithmsCourseId,
                Name = "Algorithms",
                CourseCode = "ALGR",
                Description = "Algorithms Course",
                Credits = 3,
                CourseType = CourseType.Core,
                DepartmentId = computerScienceDepartmentId
            },
            new Course
            {
                Id = musicCourseId,
                Name = "Music",
                CourseCode = "MUS",
                Description = "Music Course",
                Credits = 3,
                CourseType = CourseType.Core,
                DepartmentId = artsDepartmentId
            },
            new Course
            {
                Id = paintingCourseId,
                Name = "Painting",
                CourseCode = "PAINT",
                Description = "Painting Course",
                Credits = 3,
                CourseType = CourseType.Core,
                DepartmentId = artsDepartmentId
            },
            new Course
            {
                Id = photographyCourseId,
                Name = "Photography",
                CourseCode = "PHOTO",
                Description = "Photography Course",
                Credits = 3,
                CourseType = CourseType.Core,
                DepartmentId = artsDepartmentId
            },
            new Course
            {
                Id = danceCourseId,
                Name = "Dance",
                CourseCode = "DANCE",
                Description = "Dance Course",
                Credits = 3,
                CourseType = CourseType.Core,
                DepartmentId = artsDepartmentId
            },
            new Course
            {
                Id = artHistoryCourseId,
                Name = "Art History",
                CourseCode = "ARTH",
                Description = "Art History Course",
                Credits = 3,
                CourseType = CourseType.Elective,
                DepartmentId = artsDepartmentId
            }
        );

        var mathTeacherId = new Guid("00000000-0000-0000-0000-000000000401");
        var algebraTeacherId = new Guid("00000000-0000-0000-0000-000000000402");
        var scienceTeacherId = new Guid("00000000-0000-0000-0000-000000000403");
        var computerScienceTeacherId = new Guid("00000000-0000-0000-0000-000000000404");
        var algorithmsTeacherId = new Guid("00000000-0000-0000-0000-000000000405");
        var musicTeacherId = new Guid("00000000-0000-0000-0000-000000000406");
        var paintingTeacherId = new Guid("00000000-0000-0000-0000-000000000407");
        var photographyTeacherId = new Guid("00000000-0000-0000-0000-000000000408");
        var danceTeacherId = new Guid("00000000-0000-0000-0000-000000000409");
        modelBuilder.Entity<Teacher>().HasData(
            new Teacher
            {
                Id = mathTeacherId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@sampleschool.com",
                Phone = "111-111-1111",
                Bio = "John Doe is a teacher at Sample School.",
                DepartmentId = mathDepartmentId,
            },
            new Teacher
            {
                Id = algebraTeacherId,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "",
                Phone = "555-555-5555",
                Bio = "Jane Doe is a teacher at Sample School.",
                DepartmentId = mathDepartmentId,
            },
            new Teacher
            {
                Id = scienceTeacherId,
                FirstName = "David",
                LastName = "Doe",
                Email = "",
                Phone = "123-123-1234",
                Bio = "David Doe is a teacher at Sample School.",
                DepartmentId = scienceDepartmentId,
            },
            new Teacher
            {
                Id = computerScienceTeacherId,
                FirstName = "Bob",
                LastName = "Doe",
                Phone = "222-222-2222",
                Bio = "Bob Doe is a teacher at Sample School.",
                DepartmentId = computerScienceDepartmentId,
            },
            new Teacher
            {
                Id = algorithmsTeacherId,
                FirstName = "Jill",
                LastName = "Doe",
                Phone = "333-333-3333",
                Bio = "Jill Doe is a teacher at Sample School.",
                DepartmentId = computerScienceDepartmentId,
            },
            new Teacher
            {
                Id = musicTeacherId,
                FirstName = "Adam",
                LastName = "Doe",
                Phone = "333-333-3333",
                Bio = "Adam Doe is a teacher at Sample School.",
                DepartmentId = artsDepartmentId,
            },
            new Teacher
            {
                Id = paintingTeacherId,
                FirstName = "James",
                LastName = "Doe",
                Phone = "444-444-4444",
                Bio = "James Doe is a teacher at Sample School.",
                DepartmentId = artsDepartmentId,
            },
            new Teacher
            {
                Id = photographyTeacherId,
                FirstName = "Jenny",
                LastName = "Doe",
                Phone = "666-666-6666",
                Bio = "Jenny Doe is a teacher at Sample School.",
                DepartmentId = artsDepartmentId,
            },
            new Teacher
            {
                Id = danceTeacherId,
                FirstName = "Sara",
                LastName = "Doe",
                Phone = "777-777-7777",
                Bio = "Sara Doe is a teacher at Sample School.",
                DepartmentId = artsDepartmentId,
            }
        );

        modelBuilder.Entity<TeacherCourse>().HasData(
            new TeacherCourse { TeacherId = mathTeacherId, CourseId = mathCourseId },
            new TeacherCourse { TeacherId = mathTeacherId, CourseId = algebraCourseId },
            new TeacherCourse { TeacherId = mathTeacherId, CourseId = mathFundamentalsCourseId },
            new TeacherCourse { TeacherId = algebraTeacherId, CourseId = algebraCourseId },
            new TeacherCourse { TeacherId = scienceTeacherId, CourseId = scienceCourseId },
            new TeacherCourse { TeacherId = scienceTeacherId, CourseId = physicsCourseId },
            new TeacherCourse { TeacherId = scienceTeacherId, CourseId = chemistryCourseId },
            new TeacherCourse { TeacherId = scienceTeacherId, CourseId = environmentalScienceCourseId },
            new TeacherCourse { TeacherId = scienceTeacherId, CourseId = scienceFundamentalsCourseId },
            new TeacherCourse { TeacherId = computerScienceTeacherId, CourseId = computerScienceCourseId },
            new TeacherCourse { TeacherId = computerScienceTeacherId, CourseId = computerProgrammingCourseId },
            new TeacherCourse { TeacherId = computerScienceTeacherId, CourseId = computerApplicationsCourseId },
            new TeacherCourse { TeacherId = computerScienceTeacherId, CourseId = computerScienceFundamentalsCourseId },
            new TeacherCourse { TeacherId = computerScienceTeacherId, CourseId = dataStructuresCourseId },
            new TeacherCourse { TeacherId = algorithmsTeacherId, CourseId = algorithmsCourseId },
            new TeacherCourse { TeacherId = algorithmsTeacherId, CourseId = computerScienceFundamentalsCourseId },
            new TeacherCourse { TeacherId = algorithmsTeacherId, CourseId = dataStructuresCourseId },
            new TeacherCourse { TeacherId = musicTeacherId, CourseId = musicCourseId },
            new TeacherCourse { TeacherId = musicTeacherId, CourseId = artHistoryCourseId },
            new TeacherCourse { TeacherId = paintingTeacherId, CourseId = paintingCourseId },
            new TeacherCourse { TeacherId = paintingTeacherId, CourseId = artHistoryCourseId },
            new TeacherCourse { TeacherId = photographyTeacherId, CourseId = photographyCourseId },
            new TeacherCourse { TeacherId = danceTeacherId, CourseId = danceCourseId }
        );

        modelBuilder.Entity<LabRoom>().HasData(
            new LabRoom
            {
                Id = new Guid("00000000-0000-0000-0000-000000000501"),
                Name = "Chemistry Lab",
                Description = "Chemistry Lab",
                Capacity = 20,
                Equipment = "Chemicals, Beakers, Bunsen Burners",
                Subject = "Chemistry",
                HasChemicals = true
            },
            new LabRoom
            {
                Id = new Guid("00000000-0000-0000-0000-000000000502"),
                Name = "Physics Lab",
                Description = "Physics Lab",
                Capacity = 20,
                Equipment = "Bunsen Burners, Magnets, Prisms",
                Subject = "Physics",
                HasChemicals = false
            },
            new LabRoom
            {
                Id = new Guid("00000000-0000-0000-0000-000000000503"),
                Name = "Computer Lab",
                Description = "Computer Lab",
                Capacity = 20,
                Equipment = "Computers, Projector",
                Subject = "Computer Science",
                HasChemicals = false
            }
        );

        modelBuilder.Entity<Classroom>().HasData(
            new Classroom
            {
                Id = new Guid("00000000-0000-0000-0000-000000000601"),
                Name = "Classroom 1",
                Description = "Classroom 1",
                Capacity = 20,
                HasComputers = true,
                HasProjector = false,
                HasWhiteboard = true
            },
            new Classroom
            {
                Id = new Guid("00000000-0000-0000-0000-000000000602"),
                Name = "Classroom 2",
                Description = "Classroom 2",
                Capacity = 30,
                HasComputers = true,
                HasProjector = false,
                HasWhiteboard = true
            },
            new Classroom
            {
                Id = new Guid("00000000-0000-0000-0000-000000000603"),
                Name = "Classroom 3",
                Description = "Classroom 3",
                Capacity = 40,
                HasComputers = true,
                HasProjector = true,
                HasWhiteboard = true
            },
            new Classroom
            {
                Id = new Guid("00000000-0000-0000-0000-000000000604"),
                Name = "Classroom 4",
                Description = "Classroom 4",
                Capacity = 50,
                HasComputers = false,
                HasProjector = false,
                HasWhiteboard = true
            },
            new Classroom
            {
                Id = new Guid("00000000-0000-0000-0000-000000000605"),
                Name = "Classroom 5",
                Description = "Classroom 5",
                Capacity = 100,
                HasComputers = true,
                HasProjector = true,
                HasWhiteboard = true
            }
        );

        modelBuilder.Entity<Equipment>().HasData(
            new Equipment
            {
                Id = new Guid("00000000-0000-0000-0000-000000000701"),
                Name = "Bunsen Burner",
                Description = "Bunsen Burner",
                Condition = "Good",
                Brand = "Bunsen",
                Quantity = 10
            },
            new Equipment
            {
                Id = new Guid("00000000-0000-0000-0000-000000000702"),
                Name = "Beaker",
                Description = "Beaker",
                Condition = "Good",
                Brand = "Beaker",
                Quantity = 10
            },
            new Equipment
            {
                Id = new Guid("00000000-0000-0000-0000-000000000703"),
                Name = "Prism",
                Description = "Prism",
                Condition = "Good",
                Brand = "Prism",
                Quantity = 10
            },
            new Equipment
            {
                Id = new Guid("00000000-0000-0000-0000-000000000704"),
                Name = "Magnets",
                Description = "Magnets",
                Condition = "Good",
                Brand = "Magnets",
                Quantity = 10
            },
            new Equipment
            {
                Id = new Guid("00000000-0000-0000-0000-000000000705"),
                Name = "Computer",
                Description = "Computer",
                Condition = "Good",
                Brand = "Computer",
                Quantity = 40
            },
            new Equipment
            {
                Id = new Guid("00000000-0000-0000-0000-000000000706"),
                Name = "Projector",
                Description = "Projector",
                Condition = "Good",
                Brand = "Projector",
                Quantity = 6
            }
        );

        modelBuilder.Entity<Furniture>().HasData(
            new Furniture
            {
                Id = new Guid("00000000-0000-0000-0000-000000000801"),
                Name = "Desk",
                Description = "Desk",
                Color = "Brown",
                Material = "Wood",
                Quantity = 20
            },
            new Furniture
            {
                Id = new Guid("00000000-0000-0000-0000-000000000802"),
                Name = "Chair",
                Description = "Chair",
                Color = "Black",
                Material = "Wood",
                Quantity = 20
            },
            new Furniture
            {
                Id = new Guid("00000000-0000-0000-0000-000000000803"),
                Name = "Whiteboard",
                Description = "Whiteboard",
                Color = "White",
                Material = "Plastic",
                Quantity = 10
            }
        );

        // Generate 30 student Ids
        var studentId1 = new Guid("00000000-0000-0000-0000-000000000901");
        var studentId2 = new Guid("00000000-0000-0000-0000-000000000902");
        var studentId3 = new Guid("00000000-0000-0000-0000-000000000903");
        var studentId4 = new Guid("00000000-0000-0000-0000-000000000904");
        var studentId5 = new Guid("00000000-0000-0000-0000-000000000905");
        var studentId6 = new Guid("00000000-0000-0000-0000-000000000906");
        var studentId7 = new Guid("00000000-0000-0000-0000-000000000907");
        var studentId8 = new Guid("00000000-0000-0000-0000-000000000908");
        var studentId9 = new Guid("00000000-0000-0000-0000-000000000909");
        var studentId10 = new Guid("00000000-0000-0000-0000-000000000910");
        var studentId11 = new Guid("00000000-0000-0000-0000-000000000911");
        var studentId12 = new Guid("00000000-0000-0000-0000-000000000912");
        var studentId13 = new Guid("00000000-0000-0000-0000-000000000913");
        var studentId14 = new Guid("00000000-0000-0000-0000-000000000914");
        var studentId15 = new Guid("00000000-0000-0000-0000-000000000915");
        var studentId16 = new Guid("00000000-0000-0000-0000-000000000916");
        var studentId17 = new Guid("00000000-0000-0000-0000-000000000917");
        var studentId18 = new Guid("00000000-0000-0000-0000-000000000918");
        var studentId19 = new Guid("00000000-0000-0000-0000-000000000919");
        var studentId20 = new Guid("00000000-0000-0000-0000-000000000920");
        var studentId21 = new Guid("00000000-0000-0000-0000-000000000921");
        var studentId22 = new Guid("00000000-0000-0000-0000-000000000922");
        var studentId23 = new Guid("00000000-0000-0000-0000-000000000923");
        var studentId24 = new Guid("00000000-0000-0000-0000-000000000924");
        var studentId25 = new Guid("00000000-0000-0000-0000-000000000925");
        var studentId26 = new Guid("00000000-0000-0000-0000-000000000926");
        var studentId27 = new Guid("00000000-0000-0000-0000-000000000927");
        var studentId28 = new Guid("00000000-0000-0000-0000-000000000928");
        var studentId29 = new Guid("00000000-0000-0000-0000-000000000929");
        var studentId30 = new Guid("00000000-0000-0000-0000-000000000930");

        // Add 30 students with different names
        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = studentId1,
                FirstName = "John",
                LastName = "Doe",
                Email = "",
                GroupId = algebraGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-01")
            },
            new Student
            {
                Id = studentId2,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "",
                GroupId = algebraGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-02")
            },
            new Student
            {
                Id = studentId3,
                FirstName = "David",
                LastName = "Doe",
                Email = "",
                GroupId = algebraGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-03")
            },
            new Student
            {
                Id = studentId4,
                FirstName = "Bob",
                LastName = "Doe",
                Email = "",
                GroupId = algebraGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-04")
            },
            new Student
            {
                Id = studentId5,
                FirstName = "Jill",
                LastName = "Doe",
                Email = "",
                GroupId = chemistryGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-05")
            },
            new Student
            {
                Id = studentId6,
                FirstName = "Adam",
                LastName = "Doe",
                Email = "",
                GroupId = chemistryGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-06")
            },
            new Student
            {
                Id = studentId7,
                FirstName = "James",
                LastName = "Doe",
                Email = "",
                GroupId = chemistryGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-07")
            },
            new Student
            {
                Id = studentId8,
                FirstName = "Jenny",
                LastName = "Doe",
                Email = "",
                GroupId = chemistryGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-08")
            },
            new Student
            {
                Id = studentId9,
                FirstName = "Sara",
                LastName = "Doe",
                Email = "",
                GroupId = chemistryGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-09")
            },
            new Student
            {
                Id = studentId10,
                FirstName = "Jack",
                LastName = "Doe",
                Email = "",
                GroupId = computerProgrammingGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-10")
            },
            new Student
            {
                Id = studentId11,
                FirstName = "Andrew",
                LastName = "Doe",
                Email = "",
                GroupId = computerProgrammingGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-11")
            },
            new Student
            {
                Id = studentId12,
                FirstName = "Thomas",
                LastName = "Doe",
                Email = "",
                GroupId = computerProgrammingGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-12")
            },
            new Student
            {
                Id = studentId13,
                FirstName = "Elaine",
                LastName = "Doe",
                Email = "",
                GroupId = geometryGroupId,
                DateOfBirth = DateOnly.Parse("2001-01-13")
            },
            new Student
            {
                Id = studentId14,
                FirstName = "Eli",
                LastName = "Doe",
                Email = "",
                GroupId = geometryGroupId,
                DateOfBirth = DateOnly.Parse("2001-01-14")
            },
            new Student
            {
                Id = studentId15,
                FirstName = "Dominic",
                LastName = "Doe",
                Email = "",
                GroupId = geometryGroupId,
                DateOfBirth = DateOnly.Parse("2001-01-15")
            },
            new Student
            {
                Id = studentId16,
                FirstName = "Lily",
                LastName = "Doe",
                Email = "",
                GroupId = environmentalScienceGroupId,
                DateOfBirth = DateOnly.Parse("2001-01-16")
            },
            new Student
            {
                Id = studentId17,
                FirstName = "Liam",
                LastName = "Doe",
                Email = "",
                GroupId = environmentalScienceGroupId,
                DateOfBirth = DateOnly.Parse("2001-01-17")
            },
            new Student
            {
                Id = studentId18,
                FirstName = "Olivia",
                LastName = "Doe",
                Email = "",
                GroupId = environmentalScienceGroupId,
                DateOfBirth = DateOnly.Parse("2001-01-18")
            },
            new Student
            {
                Id = studentId19,
                FirstName = "Noah",
                LastName = "Doe",
                Email = "",
                GroupId = computerApplicationsGroupId,
                DateOfBirth = DateOnly.Parse("2001-01-19")
            },
            new Student
            {
                Id = studentId20,
                FirstName = "Emma",
                LastName = "Doe",
                Email = "",
                GroupId = computerApplicationsGroupId,
                DateOfBirth = DateOnly.Parse("2002-01-20")
            },
            new Student
            {
                Id = studentId21,
                FirstName = "Oliver",
                LastName = "Doe",
                Email = "",
                GroupId = computerApplicationsGroupId,
                DateOfBirth = DateOnly.Parse("2002-01-21")
            },
            new Student
            {
                Id = studentId22,
                FirstName = "Ava",
                LastName = "Doe",
                Email = "",
                GroupId = computerScienceGroupId,
                DateOfBirth = DateOnly.Parse("2002-01-22")
            },
            new Student
            {
                Id = studentId23,
                FirstName = "William",
                LastName = "Doe",
                Email = "",
                GroupId = computerScienceGroupId,
                DateOfBirth = DateOnly.Parse("2002-01-23")
            },
            new Student
            {
                Id = studentId24,
                FirstName = "Sophia",
                LastName = "Doe",
                Email = "",
                GroupId = computerScienceGroupId,
                DateOfBirth = DateOnly.Parse("2002-01-24")
            },
            new Student
            {
                Id = studentId25,
                FirstName = "Ethan",
                LastName = "Doe",
                Email = "",
                GroupId = musicGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-25")
            },
            new Student
            {
                Id = studentId26,
                FirstName = "Isabella",
                LastName = "Doe",
                Email = "",
                GroupId = musicGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-26")
            },
            new Student
            {
                Id = studentId27,
                FirstName = "James",
                LastName = "Doe",
                Email = "",
                GroupId = musicGroupId,
                DateOfBirth = DateOnly.Parse("2000-01-27")
            },
            new Student
            {
                Id = studentId28,
                FirstName = "Lucas",
                LastName = "Doe",
                Email = "",
                GroupId = paintingGroupId,
                DateOfBirth = DateOnly.Parse("2003-01-28")
            },
            new Student
            {
                Id = studentId29,
                FirstName = "Mia",
                LastName = "Doe",
                Email = "",
                GroupId = paintingGroupId,
                DateOfBirth = DateOnly.Parse("2003-01-29")
            },
            new Student
            {
                Id = studentId30,
                FirstName = "Alexander",
                LastName = "Doe",
                Email = "",
                GroupId = paintingGroupId,
                DateOfBirth = DateOnly.Parse("2003-01-30")
            }
        );

    }
}
