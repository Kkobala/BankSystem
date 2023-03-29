using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredAt" },
                values: new object[] { "62e49935-cf8d-4b80-ba59-7085a7c88095", "AQAAAAIAAYagAAAAEO696KeyyMWlIVrtPCMD+p3ABNYFBjhhPfszUS7pizjXB7m6PlPwCdarfTMhnYPd4Q==", new DateTime(2023, 3, 29, 9, 34, 54, 351, DateTimeKind.Utc).AddTicks(9828) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredAt" },
                values: new object[] { "ea5834e2-2dd0-424b-95a6-29f74d8e04fd", "AQAAAAIAAYagAAAAEAYbYxYwzMks7kMZbXrRbqveWzqDZDvvh6ejf3P+jxOREWj6SFpdL9ck7OPAe5gaow==", new DateTime(2023, 3, 29, 9, 2, 21, 468, DateTimeKind.Utc).AddTicks(9257) });
        }
    }
}
