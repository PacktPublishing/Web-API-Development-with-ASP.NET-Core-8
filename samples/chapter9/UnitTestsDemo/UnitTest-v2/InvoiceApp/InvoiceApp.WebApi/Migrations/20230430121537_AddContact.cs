using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceApp.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Invoices");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(64)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(64)", nullable: false),
                    Email = table.Column<string>(type: "varchar(128)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(32)", nullable: true),
                    Company = table.Column<string>(type: "varchar(128)", nullable: true),
                    Address = table.Column<string>(type: "varchar(128)", nullable: true),
                    City = table.Column<string>(type: "varchar(64)", nullable: true),
                    State = table.Column<string>(type: "varchar(64)", nullable: true),
                    Zip = table.Column<string>(type: "varchar(16)", nullable: true),
                    Country = table.Column<string>(type: "varchar(64)", nullable: true),
                    Notes = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ContactId",
                table: "Invoices",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Email",
                table: "Contacts",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Contacts_ContactId",
                table: "Invoices",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Contacts_ContactId",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ContactId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Invoices",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }
    }
}
