using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp1.Migrations
{
    /// <inheritdoc />
    public partial class AddTcats1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_dog",
                table: "dog");

            migrationBuilder.RenameTable(
                name: "dog",
                newName: "Dog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dog",
                table: "Dog",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "T_Cat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(22)", maxLength: 22, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Cat", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Cat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dog",
                table: "Dog");

            migrationBuilder.RenameTable(
                name: "Dog",
                newName: "dog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dog",
                table: "dog",
                column: "Id");
        }
    }
}
