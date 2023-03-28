using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Migrations
{
    /// <inheritdoc />
    public partial class MakeChangesTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredAt" },
                values: new object[] { "ea668fd9-91a4-461c-9f7d-8a80aeb50910", "AQAAAAIAAYagAAAAENBS6OWPC0yuyDN0DHUJdht2No98wI6ui4LjykdymQAz8jHNUDXqfiDy3eLchaMuCg==", new DateTime(2023, 3, 28, 15, 43, 52, 290, DateTimeKind.Utc).AddTicks(6511) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredAt" },
                values: new object[] { "ecd0fc0d-1580-4518-a804-6a3c9794b81b", "AQAAAAIAAYagAAAAEJznKoW46Lwu7cb9d+9qz8Cf9xf08mQZQkcmo4qI2axpxUhQ6BQNLCi5ihZkA/Y1vg==", new DateTime(2023, 3, 28, 14, 38, 26, 323, DateTimeKind.Utc).AddTicks(8049) });
        }
    }
}
