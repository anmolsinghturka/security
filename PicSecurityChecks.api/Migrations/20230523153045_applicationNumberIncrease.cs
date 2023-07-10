using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicSecurityChecks.api.Migrations
{
    /// <inheritdoc />
    public partial class applicationNumberIncrease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "PIC_StaticRecords",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "application_number",
                schema: "PIC",
                table: "SecurityCheckNames",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PIC_StaticRecords",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "application_number",
                schema: "PIC",
                table: "SecurityCheckNames",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
