using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class BackToDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "User",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "user_phone_unique",
                table: "User",
                column: "phone",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "user_phone_unique",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "User",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);
        }
    }
}
