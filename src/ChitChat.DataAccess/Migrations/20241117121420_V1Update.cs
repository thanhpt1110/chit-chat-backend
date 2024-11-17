using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChitChat.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class V1Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestMigration",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Test_Update_comment",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "InteractionType",
                table: "UserInteractions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CommentCount",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReactionCount",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentType",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReactionCount",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ReactionCount",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CommentType",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ReactionCount",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "InteractionType",
                table: "UserInteractions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TestMigration",
                table: "Comments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Test_Update_comment",
                table: "Comments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
