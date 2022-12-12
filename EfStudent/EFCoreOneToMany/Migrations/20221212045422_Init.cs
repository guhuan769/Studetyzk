using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreOneToMany.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Article",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Article", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Comments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Comments_T_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "T_Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Leave",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequesterIdId = table.Column<long>(type: "bigint", nullable: true),
                    ApproverIdId = table.Column<long>(type: "bigint", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Leave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Leave_T_User_ApproverIdId",
                        column: x => x.ApproverIdId,
                        principalTable: "T_User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_Leave_T_User_RequesterIdId",
                        column: x => x.RequesterIdId,
                        principalTable: "T_User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Comments_ArticleId",
                table: "T_Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Leave_ApproverIdId",
                table: "T_Leave",
                column: "ApproverIdId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Leave_RequesterIdId",
                table: "T_Leave",
                column: "RequesterIdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Comments");

            migrationBuilder.DropTable(
                name: "T_Leave");

            migrationBuilder.DropTable(
                name: "T_Article");

            migrationBuilder.DropTable(
                name: "T_User");
        }
    }
}
