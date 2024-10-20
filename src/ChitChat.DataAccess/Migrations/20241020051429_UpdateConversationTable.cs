using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChitChat.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConversationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationDetails_Conversations_ConversationId",
                table: "ConversationDetails");

            migrationBuilder.AddColumn<string>(
                name: "ConversationType",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumOfUser",
                table: "Conversations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationDetails_Conversations_ConversationId",
                table: "ConversationDetails",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationDetails_Conversations_ConversationId",
                table: "ConversationDetails");

            migrationBuilder.DropColumn(
                name: "ConversationType",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "NumOfUser",
                table: "Conversations");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationDetails_Conversations_ConversationId",
                table: "ConversationDetails",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
