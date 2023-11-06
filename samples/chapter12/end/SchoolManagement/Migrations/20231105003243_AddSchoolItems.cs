using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddSchoolItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Furniture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Material = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furniture", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Equipment",
                columns: new[] { "Id", "Brand", "Condition", "Description", "Name", "Quantity" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000701"), "Bunsen", "Good", "Bunsen Burner", "Bunsen Burner", 10 },
                    { new Guid("00000000-0000-0000-0000-000000000702"), "Beaker", "Good", "Beaker", "Beaker", 10 },
                    { new Guid("00000000-0000-0000-0000-000000000703"), "Prism", "Good", "Prism", "Prism", 10 },
                    { new Guid("00000000-0000-0000-0000-000000000704"), "Magnets", "Good", "Magnets", "Magnets", 10 },
                    { new Guid("00000000-0000-0000-0000-000000000705"), "Computer", "Good", "Computer", "Computer", 40 },
                    { new Guid("00000000-0000-0000-0000-000000000706"), "Projector", "Good", "Projector", "Projector", 6 }
                });

            migrationBuilder.InsertData(
                table: "Furniture",
                columns: new[] { "Id", "Color", "Description", "Material", "Name", "Quantity" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000801"), "Brown", "Desk", "Wood", "Desk", 20 },
                    { new Guid("00000000-0000-0000-0000-000000000802"), "Black", "Chair", "Wood", "Chair", 20 },
                    { new Guid("00000000-0000-0000-0000-000000000803"), "White", "Whiteboard", "Plastic", "Whiteboard", 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Furniture");
        }
    }
}
