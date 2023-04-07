using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTypeOfAmountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Accounts",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredAt" },
                values: new object[] { "8bcf983a-c131-4949-8e0e-f3657c56b2a3", "AQAAAAIAAYagAAAAENbEbWQGL1a2LXnW9cJ5lVLStjP9r247ybxAzM3A+/7XNlyvLbgBboECv7ywTY6Ljg==", new DateTime(2023, 4, 7, 8, 41, 19, 313, DateTimeKind.Utc).AddTicks(7311) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Accounts",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredAt" },
                values: new object[] { "ff7fe4e1-2ca5-4ee6-b3fd-4167705f6d28", "AQAAAAIAAYagAAAAEKt0QXGZpO88dal4QGdpm+g/rrTXe/JTXQH4dygKhIgQZmLKRpE6JxFDXEkK7S2KGA==", new DateTime(2023, 4, 7, 8, 40, 6, 110, DateTimeKind.Utc).AddTicks(4889) });
        }
    }
}
