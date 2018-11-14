using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class ChangePhoneUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "user_phone_unique",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "user_phone_unique",
                table: "User",
                column: "phone",
                unique: true,
                filter: "[phone] IS NOT NULL");
        }
    }
}
