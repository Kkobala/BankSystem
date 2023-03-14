using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Migrations
{
    /// <inheritdoc />
    public partial class modifytransaction : Migration
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

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ToAccountId",
                table: "Transactions",
                newName: "ToIBANId");

            migrationBuilder.RenameColumn(
                name: "FromAccountId",
                table: "Transactions",
                newName: "FromIBANId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ToAccountId",
                table: "Transactions",
                newName: "IX_Transactions_ToIBANId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_FromAccountId",
                table: "Transactions",
                newName: "IX_Transactions_FromIBANId");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_FromIBANId",
                table: "Transactions",
                column: "FromIBANId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_ToIBANId",
                table: "Transactions",
                column: "ToIBANId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_FromIBANId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_ToIBANId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "ToIBANId",
                table: "Transactions",
                newName: "ToAccountId");

            migrationBuilder.RenameColumn(
                name: "FromIBANId",
                table: "Transactions",
                newName: "FromAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ToIBANId",
                table: "Transactions",
                newName: "IX_Transactions_ToAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_FromIBANId",
                table: "Transactions",
                newName: "IX_Transactions_FromAccountId");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CardNumber",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_FromAccountId",
                table: "Transactions",
                column: "FromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_ToAccountId",
                table: "Transactions",
                column: "ToAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
