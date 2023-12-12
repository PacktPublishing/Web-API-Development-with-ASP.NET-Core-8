using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CqrsDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContactInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ContactEmail", "ContactPhone", "DueDate", "InvoiceDate" },
                values: new object[] { "test1@example.com", "111111111", new DateTimeOffset(new DateTime(2023, 12, 23, 9, 0, 46, 851, DateTimeKind.Unspecified).AddTicks(3424), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 23, 9, 0, 46, 851, DateTimeKind.Unspecified).AddTicks(3406), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ContactEmail", "ContactPhone", "DueDate", "InvoiceDate" },
                values: new object[] { "test2@example.com", "22222222", new DateTimeOffset(new DateTime(2023, 12, 18, 9, 0, 46, 851, DateTimeKind.Unspecified).AddTicks(3437), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 28, 9, 0, 46, 851, DateTimeKind.Unspecified).AddTicks(3433), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ContactEmail", "ContactPhone", "DueDate", "InvoiceDate" },
                values: new object[] { "test1@example.com", "111111111", new DateTimeOffset(new DateTime(2023, 12, 13, 9, 0, 46, 851, DateTimeKind.Unspecified).AddTicks(3450), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 1, 9, 0, 46, 851, DateTimeKind.Unspecified).AddTicks(3446), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "ContactEmail", "ContactPhone", "DueDate", "InvoiceDate" },
                values: new object[] { "test2@example.com", "22222222", new DateTimeOffset(new DateTime(2023, 12, 8, 9, 0, 46, 851, DateTimeKind.Unspecified).AddTicks(3461), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 2, 9, 0, 46, 851, DateTimeKind.Unspecified).AddTicks(3458), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "ContactEmail", "ContactPhone", "DueDate", "InvoiceDate" },
                values: new object[] { "test1@example.com", "111111111", new DateTimeOffset(new DateTime(2023, 12, 5, 9, 0, 46, 851, DateTimeKind.Unspecified).AddTicks(3470), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 3, 9, 0, 46, 851, DateTimeKind.Unspecified).AddTicks(3469), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DueDate", "InvoiceDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 12, 23, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2424), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 23, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2403), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DueDate", "InvoiceDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 12, 18, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2431), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 28, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2430), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DueDate", "InvoiceDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 12, 13, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2438), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 1, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2437), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DueDate", "InvoiceDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 12, 8, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2441), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 2, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2441), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DueDate", "InvoiceDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 12, 5, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2444), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 3, 0, 45, 35, 596, DateTimeKind.Unspecified).AddTicks(2444), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
