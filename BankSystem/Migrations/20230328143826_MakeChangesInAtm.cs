using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Migrations
{
    /// <inheritdoc />
    public partial class MakeChangesInAtm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredAt" },
                values: new object[] { "ecd0fc0d-1580-4518-a804-6a3c9794b81b", "AQAAAAIAAYagAAAAEJznKoW46Lwu7cb9d+9qz8Cf9xf08mQZQkcmo4qI2axpxUhQ6BQNLCi5ihZkA/Y1vg==", new DateTime(2023, 3, 28, 14, 38, 26, 323, DateTimeKind.Utc).AddTicks(8049) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredAt" },
                values: new object[] { "6f52b461-55b2-4a30-83c5-fc35311a22d8", "AQAAAAIAAYagAAAAEEtZW48XnVWL8kND/eR4LD2/kIwqLVTdBZFFRqDmVjn3e0OnKi2B8nZP5DkXaPEmOA==", new DateTime(2023, 3, 28, 13, 26, 42, 672, DateTimeKind.Utc).AddTicks(3704) });
        }
    }
}
