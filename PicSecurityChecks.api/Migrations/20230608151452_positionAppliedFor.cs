using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicSecurityChecks.api.Migrations
{
    /// <inheritdoc />
    public partial class positionAppliedFor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "positionAppliedFor",
                schema: "PIC",
                table: "SecurityCheckNames",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "positionAppliedFor",
                schema: "PIC",
                table: "SecurityCheckNames");
        }
    }
}
