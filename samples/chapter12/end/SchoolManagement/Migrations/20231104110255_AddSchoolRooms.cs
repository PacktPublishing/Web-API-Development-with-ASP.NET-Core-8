using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddSchoolRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    HasComputers = table.Column<bool>(type: "bit", nullable: false),
                    HasProjector = table.Column<bool>(type: "bit", nullable: false),
                    HasWhiteboard = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Equipment = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    HasChemicals = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabRooms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "Id", "Capacity", "Description", "HasComputers", "HasProjector", "HasWhiteboard", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000601"), 20, "Classroom 1", true, false, true, "Classroom 1" },
                    { new Guid("00000000-0000-0000-0000-000000000602"), 30, "Classroom 2", true, false, true, "Classroom 2" },
                    { new Guid("00000000-0000-0000-0000-000000000603"), 40, "Classroom 3", true, true, true, "Classroom 3" },
                    { new Guid("00000000-0000-0000-0000-000000000604"), 50, "Classroom 4", false, false, true, "Classroom 4" },
                    { new Guid("00000000-0000-0000-0000-000000000605"), 100, "Classroom 5", true, true, true, "Classroom 5" }
                });

            migrationBuilder.InsertData(
                table: "LabRooms",
                columns: new[] { "Id", "Capacity", "Description", "Equipment", "HasChemicals", "Name", "Subject" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000501"), 20, "Chemistry Lab", "Chemicals, Beakers, Bunsen Burners", true, "Chemistry Lab", "Chemistry" },
                    { new Guid("00000000-0000-0000-0000-000000000502"), 20, "Physics Lab", "Bunsen Burners, Magnets, Prisms", false, "Physics Lab", "Physics" },
                    { new Guid("00000000-0000-0000-0000-000000000503"), 20, "Computer Lab", "Computers, Projector", false, "Computer Lab", "Computer Science" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "LabRooms");
        }
    }
}
