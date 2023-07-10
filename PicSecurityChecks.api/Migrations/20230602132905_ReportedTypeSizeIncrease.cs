﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicSecurityChecks.api.Migrations
{
    /// <inheritdoc />
    public partial class ReportedTypeSizeIncrease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReportedType",
                schema: "PIC",
                table: "SecurityCheckNames",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReportedType",
                schema: "PIC",
                table: "SecurityCheckNames",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
