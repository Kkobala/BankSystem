using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTypeOfAmountColumnInAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredAt" },
                values: new object[] { "ff7fe4e1-2ca5-4ee6-b3fd-4167705f6d28", "AQAAAAIAAYagAAAAEKt0QXGZpO88dal4QGdpm+g/rrTXe/JTXQH4dygKhIgQZmLKRpE6JxFDXEkK7S2KGA==", new DateTime(2023, 4, 7, 8, 40, 6, 110, DateTimeKind.Utc).AddTicks(4889) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredAt" },
                values: new object[] { "c682fb03-be39-47a8-8068-792089c488a8", "AQAAAAIAAYagAAAAEHM7EzUWHj0RAGj5f6ZWnwfTb24o7zbfoIzyfPisgXA54g3SwiT4xGopopPzcald0g==", new DateTime(2023, 4, 6, 17, 10, 16, 972, DateTimeKind.Utc).AddTicks(9389) });
        }
    }
}
