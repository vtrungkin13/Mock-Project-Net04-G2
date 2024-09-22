using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MockNet04G2.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddNonUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Dob", "Email", "Name", "Password", "Phone", "Role" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NonUser@gmail.com", "NonUser", "$2a$11$ztxfEDgpbBFqR6wWthThiutg/ITnhjE4zYX2r75O.mRz0IhL0ay2i", "000000000", 2 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Dob", "Email", "Name", "Password", "Phone", "Role" },
                values: new object[] { 2, new DateTime(2002, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin@gmail.com", "Admin", "$2a$11$dxbEDCqupqPDo6oMkuXIj.0oP0lD5izj5QDOVhYtleb/NqGmw45wK", "0375769058", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Dob", "Email", "Name", "Password", "Phone", "Role" },
                values: new object[] { new DateTime(2002, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin@gmail.com", "Admin", "$2a$11$QViGL43ARzT.wFuuh0VRten5W4UGoVz8ALsv9nyXi9QCyBHB6Lr8O", "0375769058", 1 });
        }
    }
}
