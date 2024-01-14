using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_User_UserID",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_UserID",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Consumer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Consumer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_UserID",
                table: "Consumer",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_User_UserID",
                table: "Consumer",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
