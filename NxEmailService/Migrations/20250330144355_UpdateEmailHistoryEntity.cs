using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NxEmailService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmailHistoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Template",
                schema: "dbo",
                table: "EmailHistory",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                schema: "dbo",
                table: "EmailHistory",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "RelatedEntityId",
                schema: "dbo",
                table: "EmailHistory",
                type: "character varying(68)",
                maxLength: 68,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedEntityName",
                schema: "dbo",
                table: "EmailHistory",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedEntityId",
                schema: "dbo",
                table: "EmailHistory");

            migrationBuilder.DropColumn(
                name: "RelatedEntityName",
                schema: "dbo",
                table: "EmailHistory");

            migrationBuilder.AlterColumn<string>(
                name: "Template",
                schema: "dbo",
                table: "EmailHistory",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                schema: "dbo",
                table: "EmailHistory",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
