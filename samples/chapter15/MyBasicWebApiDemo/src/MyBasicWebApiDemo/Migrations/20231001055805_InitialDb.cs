using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyBasicWebApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5b3c2e4c-97ba-4cb8-8b23-d6b7039cb027"), "Cloud" },
                    { new Guid("e9c6ad6c-8529-404c-81de-277394129b44"), ".NET" },
                    { new Guid("ffdd0d80-3c3b-4e83-84c9-025d5650c6e5"), "DevOps" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "Content", "Title" },
                values: new object[,]
                {
                    { new Guid("26800853-5c1a-4132-aa29-98cf7a9e0cf7"), new Guid("e9c6ad6c-8529-404c-81de-277394129b44"), "Post 3 content", "Post 3" },
                    { new Guid("2b19201b-fc29-4414-b894-135145269966"), new Guid("ffdd0d80-3c3b-4e83-84c9-025d5650c6e5"), "Post 22 content", "Post 22" },
                    { new Guid("322a795b-c6cc-4199-9945-7d3ab9ad7806"), new Guid("ffdd0d80-3c3b-4e83-84c9-025d5650c6e5"), "Post 23 content", "Post 23" },
                    { new Guid("355b1887-67fe-406e-990c-e6c8ca38a17c"), new Guid("ffdd0d80-3c3b-4e83-84c9-025d5650c6e5"), "Post 21 content", "Post 21" },
                    { new Guid("3c979917-437b-406d-a784-0784170b5dd9"), new Guid("ffdd0d80-3c3b-4e83-84c9-025d5650c6e5"), "Post 26 content", "Post 26" },
                    { new Guid("52280c5b-f9a2-4916-8189-9ce641e1c3e5"), new Guid("5b3c2e4c-97ba-4cb8-8b23-d6b7039cb027"), "Post 17 content", "Post 17" },
                    { new Guid("5604b6fc-69de-443c-9a36-c09787732492"), new Guid("e9c6ad6c-8529-404c-81de-277394129b44"), "Post 4 content", "Post 4" },
                    { new Guid("5cabcaf6-1826-4bdb-a49c-dbeebba483e9"), new Guid("5b3c2e4c-97ba-4cb8-8b23-d6b7039cb027"), "Post 16 content", "Post 16" },
                    { new Guid("63833b12-a9f4-4a85-a885-de41d0761692"), new Guid("5b3c2e4c-97ba-4cb8-8b23-d6b7039cb027"), "Post 15 content", "Post 15" },
                    { new Guid("647898fd-de43-4614-9177-bcde0282b2f5"), new Guid("ffdd0d80-3c3b-4e83-84c9-025d5650c6e5"), "Post 25 content", "Post 25" },
                    { new Guid("6bf0dd43-cf69-49b2-aa62-3605015b979b"), new Guid("5b3c2e4c-97ba-4cb8-8b23-d6b7039cb027"), "Post 11 content", "Post 11" },
                    { new Guid("71e7fe2f-1354-427c-a385-432c60e420fe"), new Guid("e9c6ad6c-8529-404c-81de-277394129b44"), "Post 8 content", "Post 8" },
                    { new Guid("7e9c3110-07ca-43aa-b608-e8db044eaf62"), new Guid("5b3c2e4c-97ba-4cb8-8b23-d6b7039cb027"), "Post 20 content", "Post 20" },
                    { new Guid("7eea01a9-123d-4201-8137-881045d9e0d4"), new Guid("e9c6ad6c-8529-404c-81de-277394129b44"), "Post 6 content", "Post 6" },
                    { new Guid("9d902c31-e949-4a48-a12d-2e4bcfcefca6"), new Guid("5b3c2e4c-97ba-4cb8-8b23-d6b7039cb027"), "Post 12 content", "Post 12" },
                    { new Guid("a5a15fcd-46f9-4c38-9711-3ea72fd14544"), new Guid("e9c6ad6c-8529-404c-81de-277394129b44"), "Post 5 content", "Post 5" },
                    { new Guid("aa106b05-248a-437c-b2ee-5e1fcb1e3375"), new Guid("5b3c2e4c-97ba-4cb8-8b23-d6b7039cb027"), "Post 19 content", "Post 19" },
                    { new Guid("b36194c2-35ca-4e47-b57e-edcb080501a9"), new Guid("e9c6ad6c-8529-404c-81de-277394129b44"), "Post 1 content", "Post 1" },
                    { new Guid("b7a60a7c-7627-490f-a04d-8f7f803cc490"), new Guid("ffdd0d80-3c3b-4e83-84c9-025d5650c6e5"), "Post 24 content", "Post 24" },
                    { new Guid("ba15df11-1afd-4643-a26c-3517e62a865e"), new Guid("5b3c2e4c-97ba-4cb8-8b23-d6b7039cb027"), "Post 18 content", "Post 18" },
                    { new Guid("bb4896ee-6c4b-4479-83c8-9534b150b8cb"), new Guid("ffdd0d80-3c3b-4e83-84c9-025d5650c6e5"), "Post 30 content", "Post 30" },
                    { new Guid("c27b5903-eeb3-4e82-bf58-f2c123570649"), new Guid("ffdd0d80-3c3b-4e83-84c9-025d5650c6e5"), "Post 27 content", "Post 27" },
                    { new Guid("c3d60a94-9229-4991-a91e-942be7dc3a03"), new Guid("5b3c2e4c-97ba-4cb8-8b23-d6b7039cb027"), "Post 14 content", "Post 14" },
                    { new Guid("d328e162-f7c3-41e5-b529-7ca197f5c9b2"), new Guid("e9c6ad6c-8529-404c-81de-277394129b44"), "Post 9 content", "Post 9" },
                    { new Guid("dbfa827a-918d-4dc9-a13b-592d39896f7e"), new Guid("5b3c2e4c-97ba-4cb8-8b23-d6b7039cb027"), "Post 13 content", "Post 13" },
                    { new Guid("dd5c6ed3-197e-48c7-bcce-bce4e3c3de3d"), new Guid("ffdd0d80-3c3b-4e83-84c9-025d5650c6e5"), "Post 28 content", "Post 28" },
                    { new Guid("f9338a91-d56f-41d5-99c7-cf579f773615"), new Guid("e9c6ad6c-8529-404c-81de-277394129b44"), "Post 10 content", "Post 10" },
                    { new Guid("f9c8ec2a-b2d3-4e4f-8c29-b0bb3404b860"), new Guid("ffdd0d80-3c3b-4e83-84c9-025d5650c6e5"), "Post 29 content", "Post 29" },
                    { new Guid("fb34dada-3d45-496b-a1dd-7ed7d7599f78"), new Guid("e9c6ad6c-8529-404c-81de-277394129b44"), "Post 7 content", "Post 7" },
                    { new Guid("fd32ed91-c9d4-4cf6-9ad5-9d5182b0b0a8"), new Guid("e9c6ad6c-8529-404c-81de-277394129b44"), "Post 2 content", "Post 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
