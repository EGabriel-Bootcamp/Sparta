using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User_Management.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class creatingDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MartialStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateOfResident = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "FirstName", "Gender", "LastName", "MartialStatus", "StateOfResident" },
                values: new object[,]
                {
                    { 1, 17, "Amos", "Male", "Iqout", "Single", "Akwa Ibom" },
                    { 2, 35, "John", "Male", "Smith", "Married", "Lagos" },
                    { 3, 18, "Victor", "Female", "James", "Single", "Cross Rivers" },
                    { 4, 20, "Blessing", "Female", "James", "Single", "Rivers" },
                    { 5, 50, "Amarachi", "Female", "Favour", "Married", "Abia" },
                    { 6, 65, "Stephen", "Male", "Michael", "Married", "Delta" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
