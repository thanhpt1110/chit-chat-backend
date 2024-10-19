using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChitChat.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Warning_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostDetailTags_Posts_PostId1",
                table: "PostDetailTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostMedias_Posts_PostId1",
                table: "PostMedias");

            migrationBuilder.DropIndex(
                name: "IX_PostMedias_PostId1",
                table: "PostMedias");

            migrationBuilder.DropIndex(
                name: "IX_PostDetailTags_PostId1",
                table: "PostDetailTags");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "PostMedias");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "PostDetailTags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PostId1",
                table: "PostMedias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PostId1",
                table: "PostDetailTags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostMedias_PostId1",
                table: "PostMedias",
                column: "PostId1");

            migrationBuilder.CreateIndex(
                name: "IX_PostDetailTags_PostId1",
                table: "PostDetailTags",
                column: "PostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PostDetailTags_Posts_PostId1",
                table: "PostDetailTags",
                column: "PostId1",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostMedias_Posts_PostId1",
                table: "PostMedias",
                column: "PostId1",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
