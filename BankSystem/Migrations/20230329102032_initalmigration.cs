using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Migrations
{
    /// <inheritdoc />
    public partial class initalmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_FromAccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_ToAccountId",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredAt" },
                values: new object[] { "a7164d7d-3ddb-4362-9c03-5fead77452bd", "AQAAAAIAAYagAAAAEBA522jA3GVoXWBFAkUhoSjOxP13cOKLP6BGBTOXB9YqFn3bdlSG9sm70KSPbBLvaQ==", new DateTime(2023, 3, 29, 10, 20, 32, 529, DateTimeKind.Utc).AddTicks(1011) });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_FromAccountId",
                table: "Transactions",
                column: "FromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_ToAccountId",
                table: "Transactions",
                column: "ToAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_FromAccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_ToAccountId",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredAt" },
                values: new object[] { "5482d374-f7e8-46b7-b73b-5ce900f6c0ad", "AQAAAAIAAYagAAAAEDTKcRAIGXL+FkgALOt7hZi1abkniM/IHFyy5BM3F+1ZHjLZILMyLHCWR+CMHedWLw==", new DateTime(2023, 3, 29, 10, 15, 0, 414, DateTimeKind.Utc).AddTicks(2744) });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_FromAccountId",
                table: "Transactions",
                column: "FromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_ToAccountId",
                table: "Transactions",
                column: "ToAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
