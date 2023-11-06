using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Mathematics Department", "Mathematics" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Science Department", "Science" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Computer Science Department", "Computer Science" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Arts Department", "Arts" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseCode", "Credits", "DepartmentId", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000301"), "MATH", 3, new Guid("00000000-0000-0000-0000-000000000001"), "Mathematics Course", "Mathematics" },
                    { new Guid("00000000-0000-0000-0000-000000000302"), "ALG", 3, new Guid("00000000-0000-0000-0000-000000000001"), "Algebra Course", "Algebra" },
                    { new Guid("00000000-0000-0000-0000-000000000303"), "GEO", 3, new Guid("00000000-0000-0000-0000-000000000001"), "Geometry Course", "Geometry" },
                    { new Guid("00000000-0000-0000-0000-000000000304"), "MATHF", 3, new Guid("00000000-0000-0000-0000-000000000001"), "Math Fundamentals Course", "Math Fundamentals" },
                    { new Guid("00000000-0000-0000-0000-000000000305"), "SCI", 3, new Guid("00000000-0000-0000-0000-000000000002"), "Science Course", "Science" },
                    { new Guid("00000000-0000-0000-0000-000000000306"), "PHY", 3, new Guid("00000000-0000-0000-0000-000000000002"), "Physics Course", "Physics" },
                    { new Guid("00000000-0000-0000-0000-000000000307"), "CHEM", 3, new Guid("00000000-0000-0000-0000-000000000002"), "Chemistry Course", "Chemistry" },
                    { new Guid("00000000-0000-0000-0000-000000000308"), "ENV", 3, new Guid("00000000-0000-0000-0000-000000000002"), "Environmental Science Course", "Environmental Science" },
                    { new Guid("00000000-0000-0000-0000-000000000309"), "SCIF", 3, new Guid("00000000-0000-0000-0000-000000000002"), "Science Fundamentals Course", "Science Fundamentals" },
                    { new Guid("00000000-0000-0000-0000-000000000310"), "CS", 3, new Guid("00000000-0000-0000-0000-000000000003"), "Computer Science Course", "Computer Science" },
                    { new Guid("00000000-0000-0000-0000-000000000311"), "CP", 3, new Guid("00000000-0000-0000-0000-000000000003"), "Computer Programming Course", "Computer Programming" },
                    { new Guid("00000000-0000-0000-0000-000000000312"), "CA", 3, new Guid("00000000-0000-0000-0000-000000000003"), "Computer Applications Course", "Computer Applications" },
                    { new Guid("00000000-0000-0000-0000-000000000313"), "CSF", 3, new Guid("00000000-0000-0000-0000-000000000003"), "Computer Science Fundamentals Course", "Computer Science Fundamentals" },
                    { new Guid("00000000-0000-0000-0000-000000000314"), "DS", 3, new Guid("00000000-0000-0000-0000-000000000003"), "Data Structures Course", "Data Structures" },
                    { new Guid("00000000-0000-0000-0000-000000000315"), "ALGR", 3, new Guid("00000000-0000-0000-0000-000000000003"), "Algorithms Course", "Algorithms" },
                    { new Guid("00000000-0000-0000-0000-000000000316"), "MUS", 3, new Guid("00000000-0000-0000-0000-000000000004"), "Music Course", "Music" },
                    { new Guid("00000000-0000-0000-0000-000000000317"), "PAINT", 3, new Guid("00000000-0000-0000-0000-000000000004"), "Painting Course", "Painting" },
                    { new Guid("00000000-0000-0000-0000-000000000318"), "PHOTO", 3, new Guid("00000000-0000-0000-0000-000000000004"), "Photography Course", "Photography" },
                    { new Guid("00000000-0000-0000-0000-000000000319"), "DANCE", 3, new Guid("00000000-0000-0000-0000-000000000004"), "Dance Course", "Dance" },
                    { new Guid("00000000-0000-0000-0000-000000000320"), "ARTH", 3, new Guid("00000000-0000-0000-0000-000000000004"), "Art History Course", "Art History" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "DepartmentId", "Description", "GroupCode", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000101"), new Guid("00000000-0000-0000-0000-000000000001"), "Mathematics Group", "MATH", "Mathematics" },
                    { new Guid("00000000-0000-0000-0000-000000000102"), new Guid("00000000-0000-0000-0000-000000000001"), "Algebra Group", "ALG", "Algebra" },
                    { new Guid("00000000-0000-0000-0000-000000000103"), new Guid("00000000-0000-0000-0000-000000000001"), "Geometry Group", "GEO", "Geometry" },
                    { new Guid("00000000-0000-0000-0000-000000000201"), new Guid("00000000-0000-0000-0000-000000000002"), "Science Group", "SCI", "Science" },
                    { new Guid("00000000-0000-0000-0000-000000000202"), new Guid("00000000-0000-0000-0000-000000000002"), "Physics Group", "PHY", "Physics" },
                    { new Guid("00000000-0000-0000-0000-000000000203"), new Guid("00000000-0000-0000-0000-000000000002"), "Chemistry Group", "CHEM", "Chemistry" },
                    { new Guid("00000000-0000-0000-0000-000000000204"), new Guid("00000000-0000-0000-0000-000000000002"), "Environmental Science Group", "ENV", "Environmental Science" },
                    { new Guid("00000000-0000-0000-0000-000000000205"), new Guid("00000000-0000-0000-0000-000000000003"), "Computer Science Group", "CS", "Computer Science" },
                    { new Guid("00000000-0000-0000-0000-000000000206"), new Guid("00000000-0000-0000-0000-000000000003"), "Computer Programming Group", "CP", "Computer Programming" },
                    { new Guid("00000000-0000-0000-0000-000000000207"), new Guid("00000000-0000-0000-0000-000000000003"), "Computer Applications Group", "CA", "Computer Applications" },
                    { new Guid("00000000-0000-0000-0000-000000000208"), new Guid("00000000-0000-0000-0000-000000000004"), "Music Group", "MUS", "Music" },
                    { new Guid("00000000-0000-0000-0000-000000000209"), new Guid("00000000-0000-0000-0000-000000000004"), "Painting Group", "PAINT", "Painting" },
                    { new Guid("00000000-0000-0000-0000-000000000210"), new Guid("00000000-0000-0000-0000-000000000004"), "Photography Group", "PHOTO", "Photography" },
                    { new Guid("00000000-0000-0000-0000-000000000211"), new Guid("00000000-0000-0000-0000-000000000004"), "Dance Group", "DANCE", "Dance" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Bio", "DepartmentId", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000401"), "John Doe is a teacher at Sample School.", new Guid("00000000-0000-0000-0000-000000000001"), "john.doe@sampleschool.com", "John", "Doe", "111-111-1111" },
                    { new Guid("00000000-0000-0000-0000-000000000402"), "Jane Doe is a teacher at Sample School.", new Guid("00000000-0000-0000-0000-000000000001"), "", "Jane", "Doe", "555-555-5555" },
                    { new Guid("00000000-0000-0000-0000-000000000403"), "David Doe is a teacher at Sample School.", new Guid("00000000-0000-0000-0000-000000000002"), "", "David", "Doe", "123-123-1234" },
                    { new Guid("00000000-0000-0000-0000-000000000404"), "Bob Doe is a teacher at Sample School.", new Guid("00000000-0000-0000-0000-000000000003"), "", "Bob", "Doe", "222-222-2222" },
                    { new Guid("00000000-0000-0000-0000-000000000405"), "Jill Doe is a teacher at Sample School.", new Guid("00000000-0000-0000-0000-000000000003"), "", "Jill", "Doe", "333-333-3333" },
                    { new Guid("00000000-0000-0000-0000-000000000406"), "Adam Doe is a teacher at Sample School.", new Guid("00000000-0000-0000-0000-000000000004"), "", "Adam", "Doe", "333-333-3333" },
                    { new Guid("00000000-0000-0000-0000-000000000407"), "James Doe is a teacher at Sample School.", new Guid("00000000-0000-0000-0000-000000000004"), "", "James", "Doe", "444-444-4444" },
                    { new Guid("00000000-0000-0000-0000-000000000408"), "Jenny Doe is a teacher at Sample School.", new Guid("00000000-0000-0000-0000-000000000004"), "", "Jenny", "Doe", "666-666-6666" },
                    { new Guid("00000000-0000-0000-0000-000000000409"), "Sara Doe is a teacher at Sample School.", new Guid("00000000-0000-0000-0000-000000000004"), "", "Sara", "Doe", "777-777-7777" }
                });

            migrationBuilder.InsertData(
                table: "TeacherCourses",
                columns: new[] { "CourseId", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000301"), new Guid("00000000-0000-0000-0000-000000000401") },
                    { new Guid("00000000-0000-0000-0000-000000000302"), new Guid("00000000-0000-0000-0000-000000000401") },
                    { new Guid("00000000-0000-0000-0000-000000000302"), new Guid("00000000-0000-0000-0000-000000000402") },
                    { new Guid("00000000-0000-0000-0000-000000000304"), new Guid("00000000-0000-0000-0000-000000000401") },
                    { new Guid("00000000-0000-0000-0000-000000000305"), new Guid("00000000-0000-0000-0000-000000000403") },
                    { new Guid("00000000-0000-0000-0000-000000000306"), new Guid("00000000-0000-0000-0000-000000000403") },
                    { new Guid("00000000-0000-0000-0000-000000000307"), new Guid("00000000-0000-0000-0000-000000000403") },
                    { new Guid("00000000-0000-0000-0000-000000000308"), new Guid("00000000-0000-0000-0000-000000000403") },
                    { new Guid("00000000-0000-0000-0000-000000000309"), new Guid("00000000-0000-0000-0000-000000000403") },
                    { new Guid("00000000-0000-0000-0000-000000000310"), new Guid("00000000-0000-0000-0000-000000000404") },
                    { new Guid("00000000-0000-0000-0000-000000000311"), new Guid("00000000-0000-0000-0000-000000000404") },
                    { new Guid("00000000-0000-0000-0000-000000000312"), new Guid("00000000-0000-0000-0000-000000000404") },
                    { new Guid("00000000-0000-0000-0000-000000000313"), new Guid("00000000-0000-0000-0000-000000000404") },
                    { new Guid("00000000-0000-0000-0000-000000000313"), new Guid("00000000-0000-0000-0000-000000000405") },
                    { new Guid("00000000-0000-0000-0000-000000000314"), new Guid("00000000-0000-0000-0000-000000000404") },
                    { new Guid("00000000-0000-0000-0000-000000000314"), new Guid("00000000-0000-0000-0000-000000000405") },
                    { new Guid("00000000-0000-0000-0000-000000000315"), new Guid("00000000-0000-0000-0000-000000000405") },
                    { new Guid("00000000-0000-0000-0000-000000000316"), new Guid("00000000-0000-0000-0000-000000000406") },
                    { new Guid("00000000-0000-0000-0000-000000000317"), new Guid("00000000-0000-0000-0000-000000000407") },
                    { new Guid("00000000-0000-0000-0000-000000000318"), new Guid("00000000-0000-0000-0000-000000000408") },
                    { new Guid("00000000-0000-0000-0000-000000000319"), new Guid("00000000-0000-0000-0000-000000000409") },
                    { new Guid("00000000-0000-0000-0000-000000000320"), new Guid("00000000-0000-0000-0000-000000000406") },
                    { new Guid("00000000-0000-0000-0000-000000000320"), new Guid("00000000-0000-0000-0000-000000000407") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000303"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000201"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000202"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000203"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000204"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000205"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000206"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000207"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000208"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000209"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000210"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000211"));

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000301"), new Guid("00000000-0000-0000-0000-000000000401") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000302"), new Guid("00000000-0000-0000-0000-000000000401") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000302"), new Guid("00000000-0000-0000-0000-000000000402") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000304"), new Guid("00000000-0000-0000-0000-000000000401") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000305"), new Guid("00000000-0000-0000-0000-000000000403") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000306"), new Guid("00000000-0000-0000-0000-000000000403") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000307"), new Guid("00000000-0000-0000-0000-000000000403") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000308"), new Guid("00000000-0000-0000-0000-000000000403") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000309"), new Guid("00000000-0000-0000-0000-000000000403") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000310"), new Guid("00000000-0000-0000-0000-000000000404") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000311"), new Guid("00000000-0000-0000-0000-000000000404") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000312"), new Guid("00000000-0000-0000-0000-000000000404") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000313"), new Guid("00000000-0000-0000-0000-000000000404") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000313"), new Guid("00000000-0000-0000-0000-000000000405") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000314"), new Guid("00000000-0000-0000-0000-000000000404") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000314"), new Guid("00000000-0000-0000-0000-000000000405") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000315"), new Guid("00000000-0000-0000-0000-000000000405") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000316"), new Guid("00000000-0000-0000-0000-000000000406") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000317"), new Guid("00000000-0000-0000-0000-000000000407") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000318"), new Guid("00000000-0000-0000-0000-000000000408") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000319"), new Guid("00000000-0000-0000-0000-000000000409") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000320"), new Guid("00000000-0000-0000-0000-000000000406") });

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000320"), new Guid("00000000-0000-0000-0000-000000000407") });

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000301"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000302"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000304"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000305"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000306"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000307"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000308"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000309"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000310"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000311"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000312"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000313"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000314"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000315"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000316"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000317"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000318"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000319"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000320"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000401"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000402"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000403"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000404"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000405"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000406"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000407"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000408"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000409"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));
        }
    }
}
