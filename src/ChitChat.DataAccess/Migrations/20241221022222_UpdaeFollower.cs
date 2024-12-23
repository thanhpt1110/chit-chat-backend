using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChitChat.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdaeFollower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserFollowers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FollowerId",
                table: "UserFollowers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_FollowerId",
                table: "UserFollowers",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_UserId",
                table: "UserFollowers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowers_AspNetUsers_FollowerId",
                table: "UserFollowers",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowers_AspNetUsers_UserId",
                table: "UserFollowers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowers_AspNetUsers_FollowerId",
                table: "UserFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowers_AspNetUsers_UserId",
                table: "UserFollowers");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowers_FollowerId",
                table: "UserFollowers");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowers_UserId",
                table: "UserFollowers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserFollowers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FollowerId",
                table: "UserFollowers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
