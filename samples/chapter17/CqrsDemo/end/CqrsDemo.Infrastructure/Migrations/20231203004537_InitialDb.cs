using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CqrsDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "varchar(32)", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InvoiceDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DueDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "Amount", "ContactName", "Description", "DueDate", "InvoiceDate", "InvoiceNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 2000m, "John Doe", "Invoice 1", new DateTimeOffset(new DateTime(2023, 12, 23, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2424), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 23, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2403), new TimeSpan(0, 0, 0, 0, 0)), "INV-0001", "Draft" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 2000m, "Jane Doe", "Invoice 2", new DateTimeOffset(new DateTime(2023, 12, 18, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2431), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 28, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2430), new TimeSpan(0, 0, 0, 0, 0)), "INV-0002", "Draft" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 3500m, "John Doe", "Invoice 3", new DateTimeOffset(new DateTime(2023, 12, 13, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2438), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 1, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2437), new TimeSpan(0, 0, 0, 0, 0)), "INV-0003", "Draft" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 5500m, "Jane Doe", "Invoice 4", new DateTimeOffset(new DateTime(2023, 12, 8, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2441), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 2, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2441), new TimeSpan(0, 0, 0, 0, 0)), "INV-0004", "Draft" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), 8000m, "John Doe", "Invoice 5", new DateTimeOffset(new DateTime(2023, 12, 5, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2444), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 3, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2444), new TimeSpan(0, 0, 0, 0, 0)), "INV-0005", "Draft" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "Id", "Amount", "Description", "InvoiceId", "Name", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000011"), 1000m, "Item 1", new Guid("00000000-0000-0000-0000-000000000001"), "", 1m, 1000m },
                    { new Guid("00000000-0000-0000-0000-000000000012"), 1000m, "Item 2", new Guid("00000000-0000-0000-0000-000000000001"), "", 2m, 500m },
                    { new Guid("00000000-0000-0000-0000-000000000021"), 1000m, "Item 1", new Guid("00000000-0000-0000-0000-000000000002"), "", 1m, 1000m },
                    { new Guid("00000000-0000-0000-0000-000000000022"), 1000m, "Item 2", new Guid("00000000-0000-0000-0000-000000000002"), "", 2m, 500m },
                    { new Guid("00000000-0000-0000-0000-000000000031"), 1000m, "Item 1", new Guid("00000000-0000-0000-0000-000000000003"), "", 1m, 1000m },
                    { new Guid("00000000-0000-0000-0000-000000000032"), 1000m, "Item 2", new Guid("00000000-0000-0000-0000-000000000003"), "", 2m, 500m },
                    { new Guid("00000000-0000-0000-0000-000000000033"), 1500m, "Item 3", new Guid("00000000-0000-0000-0000-000000000003"), "", 3m, 500m },
                    { new Guid("00000000-0000-0000-0000-000000000041"), 1000m, "Item 1", new Guid("00000000-0000-0000-0000-000000000004"), "", 1m, 1000m },
                    { new Guid("00000000-0000-0000-0000-000000000042"), 1000m, "Item 2", new Guid("00000000-0000-0000-0000-000000000004"), "", 2m, 500m },
                    { new Guid("00000000-0000-0000-0000-000000000043"), 1500m, "Item 3", new Guid("00000000-0000-0000-0000-000000000004"), "", 3m, 500m },
                    { new Guid("00000000-0000-0000-0000-000000000044"), 2000m, "Item 4", new Guid("00000000-0000-0000-0000-000000000004"), "", 4m, 500m },
                    { new Guid("00000000-0000-0000-0000-000000000051"), 1000m, "Item 1", new Guid("00000000-0000-0000-0000-000000000005"), "", 1m, 1000m },
                    { new Guid("00000000-0000-0000-0000-000000000052"), 1000m, "Item 2", new Guid("00000000-0000-0000-0000-000000000005"), "", 2m, 500m },
                    { new Guid("00000000-0000-0000-0000-000000000053"), 1500m, "Item 3", new Guid("00000000-0000-0000-0000-000000000005"), "", 3m, 500m },
                    { new Guid("00000000-0000-0000-0000-000000000054"), 2000m, "Item 4", new Guid("00000000-0000-0000-0000-000000000005"), "", 4m, 500m },
                    { new Guid("00000000-0000-0000-0000-000000000055"), 2500m, "Item 5", new Guid("00000000-0000-0000-0000-000000000005"), "", 5m, 500m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNumber",
                table: "Invoices",
                column: "InvoiceNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
