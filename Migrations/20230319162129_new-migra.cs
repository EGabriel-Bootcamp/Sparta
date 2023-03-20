using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace task_three.Migrations
{
    /// <inheritdoc />
    public partial class newmigra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorsId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorsId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthorsId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "AuthorsId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorsId",
                table: "Books",
                column: "AuthorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorsId",
                table: "Books",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id");
        }
    }
}
