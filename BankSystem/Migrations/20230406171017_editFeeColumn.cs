using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Migrations
{
    /// <inheritdoc />
    public partial class editFeeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Fee",
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
                values: new object[] { "c682fb03-be39-47a8-8068-792089c488a8", "AQAAAAIAAYagAAAAEHM7EzUWHj0RAGj5f6ZWnwfTb24o7zbfoIzyfPisgXA54g3SwiT4xGopopPzcald0g==", new DateTime(2023, 4, 6, 17, 10, 16, 972, DateTimeKind.Utc).AddTicks(9389) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Fee",
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
                values: new object[] { "9c16c446-15dc-4380-99d0-7fc8b4adc41a", "AQAAAAIAAYagAAAAENEFZ+KtHJKNRgIsQ7QTE5j2whF7yfhHDSYyPk8YPnNGFsvG/I6Oxk2mB+OLQYRbnQ==", new DateTime(2023, 4, 6, 16, 42, 59, 81, DateTimeKind.Utc).AddTicks(1476) });
        }
    }
}
