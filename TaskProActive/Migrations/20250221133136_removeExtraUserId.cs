using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskProActive.Migrations
{
    /// <inheritdoc />
    public partial class removeExtraUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Users_UserId1",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_UserId1",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Tags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UserId1",
                table: "Tags",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Users_UserId1",
                table: "Tags",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
