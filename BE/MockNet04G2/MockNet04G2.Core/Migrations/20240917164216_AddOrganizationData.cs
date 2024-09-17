using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MockNet04G2.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Logo", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRENF9uv9UWIWWbExsgj7XyX58xMFAOZTzUSQ&s", "Organization A", "123-456-7890" },
                    { 2, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRENF9uv9UWIWWbExsgj7XyX58xMFAOZTzUSQ&s", "Organization B", "098-765-4321" },
                    { 3, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRENF9uv9UWIWWbExsgj7XyX58xMFAOZTzUSQ&s", "Organization C", "555-555-5555" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$iBS.oGQyqEBPi/KhO07NFu4gMe1o8G2YIMZEM18ODVpDNVvvsVFxq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$x3bOGy7vBJH9kw3YR8LPZuJPcMtkyLlCV1jCEui8KD20DLlCjeZb6");
        }
    }
}
