using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NxEmailService.Migrations
{
    /// <inheritdoc />
    public partial class InitialNxEmailService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "EmailHistory",
                schema: "dbo",
                columns: table => new
                {
                    EmailHistoryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Template = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    From = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    To = table.Column<string>(type: "text", nullable: false),
                    CC = table.Column<string>(type: "text", nullable: true),
                    BCC = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Attachments = table.Column<string>(type: "text", nullable: true),
                    IsSent = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailHistory_EmailHistoryID", x => x.EmailHistoryID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailHistory",
                schema: "dbo");
        }
    }
}
