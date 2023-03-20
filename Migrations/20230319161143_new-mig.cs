using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace task_three.Migrations
{
    /// <inheritdoc />
    public partial class newmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Books_BooksId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_BooksId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "BooksId",
                table: "Publishers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BooksId",
                table: "Publishers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_BooksId",
                table: "Publishers",
                column: "BooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Books_BooksId",
                table: "Publishers",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
