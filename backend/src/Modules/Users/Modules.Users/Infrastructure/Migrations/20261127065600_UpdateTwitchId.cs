using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Users.Infrastructure.Migrations;

/// <inheritdoc />
public partial class _20261127065600_UpdateTwitchId : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "twitch_id",
            schema: "users",
            table: "users",
            type: "text",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer");

        migrationBuilder.CreateIndex(
            name: "IX_users_twitch_id",
            schema: "users",
            table: "users",
            column: "twitch_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_users_twitch_id",
            schema: "users",
            table: "users");

        migrationBuilder.AlterColumn<int>(
            name: "twitch_id",
            schema: "users",
            table: "users",
            type: "integer",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");
    }
}
