using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystemWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToClientAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                 name: "UserId",
                 table: "ClientAccount",
                 type: "nvarchar(450)",
                 maxLength: 450,
                 nullable: true); // allow null temporarily

            migrationBuilder.CreateIndex(
                name: "IX_ClientAccount_UserId",
                table: "ClientAccount",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAccount_AspNetUsers",
                table: "ClientAccount",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull); // avoid cascading delete for now
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientAccount_AspNetUsers",
                table: "ClientAccount");

            migrationBuilder.DropIndex(
                name: "IX_ClientAccount_UserId",
                table: "ClientAccount");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ClientAccount");
        }
    }
}
