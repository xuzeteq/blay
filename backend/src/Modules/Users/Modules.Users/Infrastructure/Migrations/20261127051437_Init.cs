using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Modules.Users.Infrastructure.Migrations;

/// <inheritdoc />
public partial class _20261127051437_Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "users");

        migrationBuilder.CreateTable(
            name: "users",
            schema: "users",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                twitch_id = table.Column<int>(type: "integer", nullable: false),
                username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                avatar_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_users", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "users",
            schema: "users");
    }
}
