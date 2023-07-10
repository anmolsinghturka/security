using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicSecurityChecks.api.Migrations
{
    /// <inheritdoc />
    public partial class CheckNameBoolean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CheckName",
                schema: "PIC",
                table: "SecurityCheckNames",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckName",
                schema: "PIC",
                table: "SecurityCheckNames");
        }
    }
}
