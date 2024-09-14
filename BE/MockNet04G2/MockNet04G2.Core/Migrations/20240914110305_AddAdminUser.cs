using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MockNet04G2.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Dob", "Email", "Name", "Password", "Phone", "Role" },
                values: new object[] { 1, new DateTime(2002, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin@gmail.com", "Admin", "$2a$11$K6Ha2QeXSlfSoBrzh5lA6e.FJJvTcN90EmLg8SxvrOGMQ0aN5gfi2", "0375769058", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
