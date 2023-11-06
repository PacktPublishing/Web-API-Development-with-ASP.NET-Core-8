using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Students",
                type: "date",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateOfBirth", "Email", "FirstName", "Grade", "GroupId", "LastName", "Phone" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000901"), new DateOnly(2000, 1, 1), "", "John", "", new Guid("00000000-0000-0000-0000-000000000102"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000902"), new DateOnly(2000, 1, 2), "", "Jane", "", new Guid("00000000-0000-0000-0000-000000000102"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000903"), new DateOnly(2000, 1, 3), "", "David", "", new Guid("00000000-0000-0000-0000-000000000102"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000904"), new DateOnly(2000, 1, 4), "", "Bob", "", new Guid("00000000-0000-0000-0000-000000000102"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000905"), new DateOnly(2000, 1, 5), "", "Jill", "", new Guid("00000000-0000-0000-0000-000000000203"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000906"), new DateOnly(2000, 1, 6), "", "Adam", "", new Guid("00000000-0000-0000-0000-000000000203"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000907"), new DateOnly(2000, 1, 7), "", "James", "", new Guid("00000000-0000-0000-0000-000000000203"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000908"), new DateOnly(2000, 1, 8), "", "Jenny", "", new Guid("00000000-0000-0000-0000-000000000203"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000909"), new DateOnly(2000, 1, 9), "", "Sara", "", new Guid("00000000-0000-0000-0000-000000000203"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000910"), new DateOnly(2000, 1, 10), "", "Jack", "", new Guid("00000000-0000-0000-0000-000000000206"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000911"), new DateOnly(2000, 1, 11), "", "Andrew", "", new Guid("00000000-0000-0000-0000-000000000206"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000912"), new DateOnly(2000, 1, 12), "", "Thomas", "", new Guid("00000000-0000-0000-0000-000000000206"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000913"), new DateOnly(2001, 1, 13), "", "Elaine", "", new Guid("00000000-0000-0000-0000-000000000103"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000914"), new DateOnly(2001, 1, 14), "", "Eli", "", new Guid("00000000-0000-0000-0000-000000000103"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000915"), new DateOnly(2001, 1, 15), "", "Dominic", "", new Guid("00000000-0000-0000-0000-000000000103"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000916"), new DateOnly(2001, 1, 16), "", "Lily", "", new Guid("00000000-0000-0000-0000-000000000204"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000917"), new DateOnly(2001, 1, 17), "", "Liam", "", new Guid("00000000-0000-0000-0000-000000000204"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000918"), new DateOnly(2001, 1, 18), "", "Olivia", "", new Guid("00000000-0000-0000-0000-000000000204"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000919"), new DateOnly(2001, 1, 19), "", "Noah", "", new Guid("00000000-0000-0000-0000-000000000207"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000920"), new DateOnly(2002, 1, 20), "", "Emma", "", new Guid("00000000-0000-0000-0000-000000000207"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000921"), new DateOnly(2002, 1, 21), "", "Oliver", "", new Guid("00000000-0000-0000-0000-000000000207"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000922"), new DateOnly(2002, 1, 22), "", "Ava", "", new Guid("00000000-0000-0000-0000-000000000205"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000923"), new DateOnly(2002, 1, 23), "", "William", "", new Guid("00000000-0000-0000-0000-000000000205"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000924"), new DateOnly(2002, 1, 24), "", "Sophia", "", new Guid("00000000-0000-0000-0000-000000000205"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000925"), new DateOnly(2000, 1, 25), "", "Ethan", "", new Guid("00000000-0000-0000-0000-000000000208"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000926"), new DateOnly(2000, 1, 26), "", "Isabella", "", new Guid("00000000-0000-0000-0000-000000000208"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000927"), new DateOnly(2000, 1, 27), "", "James", "", new Guid("00000000-0000-0000-0000-000000000208"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000928"), new DateOnly(2003, 1, 28), "", "Lucas", "", new Guid("00000000-0000-0000-0000-000000000209"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000929"), new DateOnly(2003, 1, 29), "", "Mia", "", new Guid("00000000-0000-0000-0000-000000000209"), "Doe", null },
                    { new Guid("00000000-0000-0000-0000-000000000930"), new DateOnly(2003, 1, 30), "", "Alexander", "", new Guid("00000000-0000-0000-0000-000000000209"), "Doe", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000901"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000902"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000903"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000904"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000905"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000906"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000907"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000908"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000909"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000910"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000911"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000912"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000913"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000914"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000915"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000916"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000917"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000918"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000919"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000920"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000921"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000922"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000923"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000924"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000925"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000926"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000927"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000928"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000929"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000930"));

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Students");
        }
    }
}
