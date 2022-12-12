using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreOneToMany.Migrations
{
    /// <inheritdoc />
    public partial class Init1111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Leave_T_User_ApproverIdId",
                table: "T_Leave");

            migrationBuilder.AlterColumn<long>(
                name: "ApproverIdId",
                table: "T_Leave",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_T_Leave_T_User_ApproverIdId",
                table: "T_Leave",
                column: "ApproverIdId",
                principalTable: "T_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Leave_T_User_ApproverIdId",
                table: "T_Leave");

            migrationBuilder.AlterColumn<long>(
                name: "ApproverIdId",
                table: "T_Leave",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Leave_T_User_ApproverIdId",
                table: "T_Leave",
                column: "ApproverIdId",
                principalTable: "T_User",
                principalColumn: "Id");
        }
    }
}
