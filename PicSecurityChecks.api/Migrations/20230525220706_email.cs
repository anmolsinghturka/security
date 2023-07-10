using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicSecurityChecks.api.Migrations
{
    /// <inheritdoc />
    public partial class email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "CheckName",
                schema: "PIC",
                table: "SecurityCheckNames",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                schema: "PIC",
                table: "SecurityCheckNames",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                schema: "PIC",
                table: "SecurityCheckNames");

            migrationBuilder.AlterColumn<bool>(
                name: "CheckName",
                schema: "PIC",
                table: "SecurityCheckNames",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
