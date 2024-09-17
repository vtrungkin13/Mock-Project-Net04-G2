using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MockNet04G2.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCampaignToDelete2Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationName",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "OrganizationPhone",
                table: "Campaigns");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$luh8dwAggAjTxRptVAYxXu.kKEL9LQGjRFFcniziYEadfW6SsNqWq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizationName",
                table: "Campaigns",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrganizationPhone",
                table: "Campaigns",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Lx5fK6mmYKmYeMLLo.r6SODZwwNKzlR6v8AB9Jftri0Sq4GRcehHO");
        }
    }
}
