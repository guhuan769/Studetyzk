using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreOneToMany.Migrations
{
    /// <inheritdoc />
    public partial class Init11113 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Leave_T_User_ApproverIdId",
                table: "T_Leave");

            migrationBuilder.DropForeignKey(
                name: "FK_T_Leave_T_User_RequesterIdId",
                table: "T_Leave");

            migrationBuilder.AlterColumn<long>(
                name: "RequesterIdId",
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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_T_Leave_T_User_RequesterIdId",
                table: "T_Leave",
                column: "RequesterIdId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_T_Leave_T_User_RequesterIdId",
                table: "T_Leave");

            migrationBuilder.AlterColumn<long>(
                name: "RequesterIdId",
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_Leave_T_User_RequesterIdId",
                table: "T_Leave",
                column: "RequesterIdId",
                principalTable: "T_User",
                principalColumn: "Id");
        }
    }
}
